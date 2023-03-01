using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace ReactWithDotNet;

record TsProperty(string comment, string propertyName, string propertyType);

class TsTypeReference
{
    public string Name { get; set; }
    public bool HasUnionValues { get; set; }
    public List<string> OptionalValues { get; set; }
    public bool IsAlfaNumeric { get; set; }
    public bool IsQuotedString { get; set; }
    public IReadOnlyList<TsTypeReference> GenericArguments { get; set; }
}

static  class TsParser
{

    
    static (bool hasRead, int newIndex, string value) ReadQuotedString(string content, int startIndex)
    {
        var i = startIndex;
        
        if (isStringStartOrEnd(content[i]))
        {
            i++;

            var charList = new List<char>();

            while (content.Length > i)
            {
                var c = content[i];
                if (char.IsLetterOrDigit(c))
                {
                    charList.Add(c);
                    i++;
                    continue;
                }
                break;
            }

            if (content.Length > i && isStringStartOrEnd(content[i]))
            {
                i++;
                if (charList.Count > 0)
                {
                    return (true,i, new string(charList.ToArray()));
                }
            }
        }

        return (false,0,null);

        static bool isStringStartOrEnd(char chr) => chr == '"' || chr == "'"[0];
    }

    static (bool hasRead, int newCursor, IReadOnlyList<TsTypeReference> tsTypeReferences) TryReadGenericTypeReferenceArguments(string content, int startIndex)
    {
        var hasRead = false;

        var tsTypeReferences = new List<TsTypeReference>();

        var i = startIndex;

        while (i < content.Length)
        {
            var readTypeReferenceOutput = TryReadTypeReference(content, i);
            if (readTypeReferenceOutput.hasRead)
            {
                hasRead = true;
                
                i = readTypeReferenceOutput.newCursor;

                tsTypeReferences.Add(readTypeReferenceOutput.tsTypeReference);
                continue;
            }
            break;
        }

        return (hasRead, i, tsTypeReferences);


    }

    static (bool hasRead, int newCursor, TsTypeReference tsTypeReference) TryFullReadTypeReference(string content, int startIndex)
    {
        
        var i = startIndex;

        var readTypeReferenceOutput = TryReadTypeReference(content, i);
        if (readTypeReferenceOutput.hasRead)
        {
            
            var typeReference = readTypeReferenceOutput.tsTypeReference;
            
            i = readTypeReferenceOutput.newCursor;

            readSpaces();

            if (content.Length > i &&  content[i] == '<')
            {
                typeReference.HasUnionValues = true;
                i++;

                var tryReadGenericTypeReferenceArgumentsOutput = TryReadGenericTypeReferenceArguments(content, i);
                if (tryReadGenericTypeReferenceArgumentsOutput.hasRead)
                {
                     i = tryReadGenericTypeReferenceArgumentsOutput.newCursor;

                     typeReference.GenericArguments = tryReadGenericTypeReferenceArgumentsOutput.tsTypeReferences;
                }
            }

            if (content[i] == '>')
            {
                i++;
            }

            return (hasRead:true, i, typeReference);
        }

       

        return (false, 0, null);

       


        void readSpaces()
        {
            var (hasRead, cursor, _) = TryReadWhile(content, i, c => c == ' ');
            if (hasRead)
            {
                i = cursor;
            }
        }




        (bool hasRead, int cursor, string value) readAlfanumeric() => TryReadWhile(content, i, char.IsLetterOrDigit);

    }
    static (bool hasRead, int newCursor, TsTypeReference tsTypeReference) TryReadTypeReference(string content, int startIndex)
    {
        var i = startIndex;

        readSpaces();

        var readAlfaNumericOutput = TryReadWhile(content, i, char.IsLetterOrDigit);
        if (readAlfaNumericOutput.hasRead)
        {
            return (true, readAlfaNumericOutput.cursor, new TsTypeReference { Name = readAlfaNumericOutput.value, IsAlfaNumeric = true });
        }

        var readQuotedString = ReadQuotedString(content, i);
        if (readQuotedString.hasRead)
        {
            return (true, readQuotedString.newIndex, new TsTypeReference { Name = readQuotedString.value, IsQuotedString = true });
        }
        
        return (false, startIndex, null);


        void readSpaces()
        {
            var (hasRead, cursor, _) = TryReadWhile(content, i, c => c == ' ');
            if (hasRead)
            {
                i = cursor;
            }
        }

    }

   

   

    /// <summary>
    /// https://primereact.org/avatar/
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static (int cursor, IReadOnlyList<TsProperty> property) TryReadProperties(string content, int cursor)
    {
        var properties = new List<TsProperty>();


        var hasRead = true;
        while (hasRead)
        {
            var (cursorNext, property) = TryReadProperty(content,cursor);

            hasRead = property is not null;

            cursor = cursorNext;
            if (hasRead)
            {
                properties.Add(property);
            }
        }

        

        return (cursor, properties);
    }

   

    static (int cursor, TsProperty property) TryReadProperty(string content, int cursor)
    {
        var cursorInitialValue = cursor;
        
        cursor = TryReadWhile(content, cursor, c => c == '\n' || c == '\r' || c == ' ').cursor;

        var (endIndexOfComment, comment) = TryReadComment(content, cursor);

        cursor = endIndexOfComment;

        cursor = TryReadWhile(content, cursor, c => c == '\n' || c == '\r' || c == ' ').cursor;


        var (endIndexOfPropertyName, propertyName)=TryReadUntil(content, cursor, ':');

        cursor = endIndexOfPropertyName;


        cursor = TryReadWhile(content, cursor, c => c == '\n' || c == '\r' || c == ' ').cursor;

        var (endIndexOfPropertyType, propertyType) = TryReadUntil(content, cursor, ';');

        if (propertyName == null)
        {
            return (cursorInitialValue, null);
        }
        
        return (endIndexOfPropertyType, new (comment, propertyName, propertyType));
    }

    static (bool hasRead, int cursor, string spaceValue) TryReadSpaces(string content, int cursor)
    {
        return TryReadWhile(content,cursor, c=>c==' ');
    }

    static (bool hasRead,int cursor, string value) TryReadWhile(string content, int cursor, Func<char,bool> isOk)
    {
        var hasRead = false;
        
        var value = "";

        while (content.Length > cursor &&  isOk(content[cursor]))
        {
            hasRead =  true;
            
            value   += content[cursor];
            
            cursor++;
        }

        return (hasRead, cursor, value);
    }

    static (bool hasRead, int cursor, char value) TryRead(string content, int cursor, Func<char, bool> isOk)
    {
        if (content.Length > cursor && isOk(content[cursor]))
        {
            return (hasRead: true, cursor + 1, content[cursor]);
        }

        return (hasRead: false, cursor+1, content[cursor]);
    }


    static (int cursor, string comment) TryReadComment(string content, int cursor)
    {
        if (content.Length <= cursor + 3)
        {
            return (cursor, null);
        }
        if (content.Substring(cursor,3) =="/**")
        {
            var endIndex = content.IndexOf("*/", cursor+3, StringComparison.OrdinalIgnoreCase);
            if (endIndex > 0)
            {
                endIndex += 2;
                
                return (endIndex, content.Substring(cursor, endIndex-cursor));
            }
        }

        return (cursor, null);
    }

    static (int cursor, string name) TryReadPropertyName(string content, int cursor)
    {
        return TryReadUntil(content, cursor, ':');
    }

    static (int cursor, string name) TryReadUntil(string content, int cursor, char finishChar)
    {
        var endIndex = content.IndexOf(finishChar,startIndex: cursor);
        if (endIndex > cursor)
        {
            return (endIndex + 1, content.Substring(cursor, endIndex - cursor));
        }

        return (cursor, null);
    }
}