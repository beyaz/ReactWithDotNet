import React from 'react';

import SyntaxHighlighter from 'react-syntax-highlighter';
import    {        a11yDark                
,a11yLight               
,agate                   
,anOldHope               
,androidstudio           
,arduinoLight            
,arta                    
,ascetic                 
,atelierCaveDark         
,atelierCaveLight        
,atelierDuneDark         
,atelierDuneLight        
,atelierEstuaryDark      
,atelierEstuaryLight     
,atelierForestDark       
,atelierForestLight      
,atelierHeathDark        
,atelierHeathLight       
,atelierLakesideDark     
,atelierLakesideLight    
,atelierPlateauDark      
,atelierPlateauLight     
,atelierSavannaDark      
,atelierSavannaLight     
,atelierSeasideDark      
,atelierSeasideLight     
,atelierSulphurpoolDark  
,atelierSulphurpoolLight 
,atomOneDarkReasonable   
,atomOneDark             
,atomOneLight            
,brownPaper              
,codepenEmbed            
,colorBrewer             
,darcula                 
,dark                    
,defaultStyle            
,docco                   
,dracula                 
,far                     
,foundation              
,githubGist              
,github                  
,gml                     
,googlecode              
,gradientDark            
,grayscale               
,gruvboxDark             
,gruvboxLight            
,hopscotch               
,hybrid                  
,idea                    
,irBlack                 
,isblEditorDark          
,isblEditorLight         
,kimbieDark              
,kimbieLight             
,lightfair               
,lioshi                  
,magula                  
,monoBlue                
,monokaiSublime          
,monokai                 
,nightOwl                
,nnfxDark                
,nnfx                    
,nord                    
,obsidian                
,ocean                   
,paraisoDark             
,paraisoLight            
,pojoaque                
,purebasic               
,qtcreatorDark           
,qtcreatorLight          
,railscasts              
,rainbow                 
,routeros                
,schoolBook              
,shadesOfPurple          
,solarizedDark           
,solarizedLight          
,srcery                  
,sunburst                
,tomorrowNightBlue       
,tomorrowNightBright     
,tomorrowNightEighties   
,tomorrowNight           
,tomorrow                
,vs                      
,vs2015                  
,xcode                   
,xt256                   
,zenburn                 

} from "react-syntax-highlighter/dist/esm/styles/hljs";

function CalculateStyle(styleName)
{
    
    if(styleName === 'a11yDark')
    {
       return a11yDark;
    }
    if(styleName === 'a11yLight')
    {
       return a11yLight;
    }
    if(styleName === 'agate')
    {
       return agate;
    }
    if(styleName === 'anOldHope')
    {
       return anOldHope;
    }
    if(styleName === 'androidstudio')
    {
       return androidstudio;
    }
    if(styleName === 'arduinoLight')
    {
       return arduinoLight;
    }
    if(styleName === 'arta')
    {
       return arta;
    }
    if(styleName === 'ascetic')
    {
       return ascetic;
    }
    if(styleName === 'atelierCaveDark')
    {
       return atelierCaveDark;
    }
    if(styleName === 'atelierCaveLight')
    {
       return atelierCaveLight;
    }
    if(styleName === 'atelierDuneDark')
    {
       return atelierDuneDark;
    }
    if(styleName === 'atelierDuneLight')
    {
       return atelierDuneLight;
    }
    if(styleName === 'atelierEstuaryDark')
    {
       return atelierEstuaryDark;
    }
    if(styleName === 'atelierEstuaryLight')
    {
       return atelierEstuaryLight;
    }
    if(styleName === 'atelierForestDark')
    {
       return atelierForestDark;
    }
    if(styleName === 'atelierForestLight')
    {
       return atelierForestLight;
    }
    if(styleName === 'atelierHeathDark')
    {
       return atelierHeathDark;
    }
    if(styleName === 'atelierHeathLight')
    {
       return atelierHeathLight;
    }
    if(styleName === 'atelierLakesideDark')
    {
       return atelierLakesideDark;
    }
    if(styleName === 'atelierLakesideLight')
    {
       return atelierLakesideLight;
    }
    if(styleName === 'atelierPlateauDark')
    {
       return atelierPlateauDark;
    }
    if(styleName === 'atelierPlateauLight')
    {
       return atelierPlateauLight;
    }
    if(styleName === 'atelierSavannaDark')
    {
       return atelierSavannaDark;
    }
    if(styleName === 'atelierSavannaLight')
    {
       return atelierSavannaLight;
    }
    if(styleName === 'atelierSeasideDark')
    {
       return atelierSeasideDark;
    }
    if(styleName === 'atelierSeasideLight')
    {
       return atelierSeasideLight;
    }
    if(styleName === 'atelierSulphurpoolDark')
    {
       return atelierSulphurpoolDark;
    }
    if(styleName === 'atelierSulphurpoolLight')
    {
       return atelierSulphurpoolLight;
    }
    if(styleName === 'atomOneDarkReasonable')
    {
       return atomOneDarkReasonable;
    }
    if(styleName === 'atomOneDark')
    {
       return atomOneDark;
    }
    if(styleName === 'atomOneLight')
    {
       return atomOneLight;
    }
    if(styleName === 'brownPaper')
    {
       return brownPaper;
    }
    if(styleName === 'codepenEmbed')
    {
       return codepenEmbed;
    }
    if(styleName === 'colorBrewer')
    {
       return colorBrewer;
    }
    if(styleName === 'darcula')
    {
       return darcula;
    }
    if(styleName === 'dark')
    {
       return dark;
    }
    if(styleName === 'defaultStyle')
    {
       return defaultStyle;
    }
    if(styleName === 'docco')
    {
       return docco;
    }
    if(styleName === 'dracula')
    {
       return dracula;
    }
    if(styleName === 'far')
    {
       return far;
    }
    if(styleName === 'foundation')
    {
       return foundation;
    }
    if(styleName === 'githubGist')
    {
       return githubGist;
    }
    if(styleName === 'github')
    {
       return github;
    }
    if(styleName === 'gml')
    {
       return gml;
    }
    if(styleName === 'googlecode')
    {
       return googlecode;
    }
    if(styleName === 'gradientDark')
    {
       return gradientDark;
    }
    if(styleName === 'grayscale')
    {
       return grayscale;
    }
    if(styleName === 'gruvboxDark')
    {
       return gruvboxDark;
    }
    if(styleName === 'gruvboxLight')
    {
       return gruvboxLight;
    }
    if(styleName === 'hopscotch')
    {
       return hopscotch;
    }
    if(styleName === 'hybrid')
    {
       return hybrid;
    }
    if(styleName === 'idea')
    {
       return idea;
    }
    if(styleName === 'irBlack')
    {
       return irBlack;
    }
    if(styleName === 'isblEditorDark')
    {
       return isblEditorDark;
    }
    if(styleName === 'isblEditorLight')
    {
       return isblEditorLight;
    }
    if(styleName === 'kimbieDark')
    {
       return kimbieDark;
    }
    if(styleName === 'kimbieLight')
    {
       return kimbieLight;
    }
    if(styleName === 'lightfair')
    {
       return lightfair;
    }
    if(styleName === 'lioshi')
    {
       return lioshi;
    }
    if(styleName === 'magula')
    {
       return magula;
    }
    if(styleName === 'monoBlue')
    {
       return monoBlue;
    }
    if(styleName === 'monokaiSublime')
    {
       return monokaiSublime;
    }
    if(styleName === 'monokai')
    {
       return monokai;
    }
    if(styleName === 'nightOwl')
    {
       return nightOwl;
    }
    if(styleName === 'nnfxDark')
    {
       return nnfxDark;
    }
    if(styleName === 'nnfx')
    {
       return nnfx;
    }
    if(styleName === 'nord')
    {
       return nord;
    }
    if(styleName === 'obsidian')
    {
       return obsidian;
    }
    if(styleName === 'ocean')
    {
       return ocean;
    }
    if(styleName === 'paraisoDark')
    {
       return paraisoDark;
    }
    if(styleName === 'paraisoLight')
    {
       return paraisoLight;
    }
    if(styleName === 'pojoaque')
    {
       return pojoaque;
    }
    if(styleName === 'purebasic')
    {
       return purebasic;
    }
    if(styleName === 'qtcreatorDark')
    {
       return qtcreatorDark;
    }
    if(styleName === 'qtcreatorLight')
    {
       return qtcreatorLight;
    }
    if(styleName === 'railscasts')
    {
       return railscasts;
    }
    if(styleName === 'rainbow')
    {
       return rainbow;
    }
    if(styleName === 'routeros')
    {
       return routeros;
    }
    if(styleName === 'schoolBook')
    {
       return schoolBook;
    }
    if(styleName === 'shadesOfPurple')
    {
       return shadesOfPurple;
    }
    if(styleName === 'solarizedDark')
    {
       return solarizedDark;
    }
    if(styleName === 'solarizedLight')
    {
       return solarizedLight;
    }
    if(styleName === 'srcery')
    {
       return srcery;
    }
    if(styleName === 'sunburst')
    {
       return sunburst;
    }
    if(styleName === 'tomorrowNightBlue')
    {
       return tomorrowNightBlue;
    }
    if(styleName === 'tomorrowNightBright')
    {
       return tomorrowNightBright;
    }
    if(styleName === 'tomorrowNightEighties')
    {
       return tomorrowNightEighties;
    }
    if(styleName === 'tomorrowNight')
    {
       return tomorrowNight;
    }
    if(styleName === 'tomorrow')
    {
       return tomorrow;
    }
    if(styleName === 'vs')
    {
       return vs;
    }
    if(styleName === 'vs2015')
    {
       return vs2015;
    }
    if(styleName === 'xcode')
    {
       return xcode;
    }
    if(styleName === 'xt256')
    {
       return xt256;
    }
    if(styleName === 'zenburn')
    {
       return zenburn;
    }


    throw styleName;
}

const Editor = React.forwardRef((props, ref) => (

    <SyntaxHighlighter ref={ref} language={props.language} style={CalculateStyle(props.style)}>
       {props.children}
    </SyntaxHighlighter>

));

export default Editor


