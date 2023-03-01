using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class ParserTests
{
    [TestMethod]
    public void A()
    {
        var input = @" /**
     * The system prop that allows defining system overrides as well as additional CSS styles.
     */
    sx?: SxProps<Theme>;

/**
     * The position of the icon relative to the label.
     * @default 'top'
     */
    iconPosition?: 'top' | 'bottom' | 'start' | 'end';

/**
     * Whether to automatically manage layering.
     * @defaultValue true
     */
    autoZIndex?: boolean | undefined;

    /**
     * Base zIndex value to use in layering.
     * @defaultValue 0
     */
    baseZIndex?: number | undefined;

";


        var cursor = 0;
        
        var properties = TryReadProperties(input, cursor);
    }

    /// <summary>
    /// https://primereact.org/avatar/
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cursor"></param>
    /// <returns></returns>
    static (int cursor, IReadOnlyList<TsProperty> property) TryReadProperties(string content, int cursor)
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

    record TsProperty(string comment, string propertyName, string propertyType);

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

    static (int cursor, string spaceValue) TryReadSpaces(string content, int cursor)
    {
        return TryReadWhile(content,cursor, c=>c==' ');
    }

    static (int cursor, string value) TryReadWhile(string content, int cursor, Func<char,bool> isOk)
    {
        var value = "";

        while (content.Length > cursor &&  isOk(content[cursor]))
        {
            value += content[cursor];
            cursor++;
        }

        return (cursor, value);
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