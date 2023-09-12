namespace ReactWithDotNet.ThirdPartyLibraries.React_Player;

public sealed class ReactPlayer : ElementBase
{
    

    /// <summary>
    /// The url of a video or song to play
    /// </summary>
    [ReactProp]
    public string url { get; set; }

   

    /// <summary>
    /// Set to true or false to pause or play the media.
    /// <br/>
    /// Default: false
    /// </summary>
    [ReactProp]
    public bool? playing { get; set; }
    
    
    /// <summary>
    /// Set to true or false to display native player controls.
    /// <br/>
    /// For Vimeo videos, hiding controls must be enabled by the video owner.
    /// <br/>
    /// Default: false
    /// </summary>
    [ReactProp]
    public bool? controls { get; set; }
    
    
    /// <summary>
    /// Applies the playsinline attribute where supported
    /// <br/>
    /// Default: false
    /// </summary>
    [ReactProp]
    public bool? playsinline { get; set; }
    
    
    
    /// <summary>
    /// Set to true to show just the video thumbnail, which loads the full player on click
    /// <br/>
    ///  Pass in an image URL to override the preview image
    /// </summary>
    [ReactProp]
    public bool? light { get; set; }
    
    
    /// <summary>
    /// Set the volume of the player, between 0 and 1
    /// <br/>
    ///  null uses default volume on all players
    /// </summary>
    [ReactProp]
    public double? volume { get; set; }
    
    
    /// <summary>
    /// Mutes the player
    /// <br/>
    ///  Only works if volume is set
    /// <br/>
    ///  Default: False
    /// </summary>
    [ReactProp]
    public bool? muted { get; set; }
    
    /// <summary>
    /// Set the playback rate of the player
    /// <br/>
    ///  Only supported by YouTube, Wistia, and file paths
    /// <br/>
    ///  Default: 1
    /// </summary>
    [ReactProp]
    public bool? playbackRate { get; set; }
    
    
    
    /// <summary>
    /// Set the width of the player
    /// <br/>
    ///  Default: 640px
    /// </summary>
    [ReactProp]
    public string width { get; set; }
    
    
    /// <summary>
    /// Set the height of the player
    /// <br/>
    ///  Default: 360px
    /// </summary>
    [ReactProp]
    public string height { get; set; }
    
    
    /// <summary>
    /// Set to true or false to enable or disable picture-in-picture mode
    /// <br/>
    ///   Only available when playing file URLs in certain browsers
    /// <br/>
    ///  Default: false
    /// </summary>
    [ReactProp]
    public bool? pip { get; set; }
    
    
    /// <summary>
    /// If you are using pip you may want to use stopOnUnmount={false} to continue playing in picture-in-picture mode even after ReactPlayer unmounts
    /// <br/>
    ///  Default: true
    /// </summary>
    [ReactProp]
    public bool? stopOnUnmount { get; set; }
    
    
    /// <summary>
    /// Override options for the various players, see config prop
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic config { get; } = new ExpandoObject();
    
    
    
    /// <summary>
    /// Called when media is loaded and ready to play. If playing is set to true, media will play immediately
    /// </summary>
    [ReactProp]
    public Action onReady { get; set; }
    
    /// <summary>
    /// Called when media starts playing
    /// </summary>
    [ReactProp]
    public Action onStart { get; set; }
    
    
      
    /// <summary>
    /// Called when media starts or resumes playing after pausing or buffering
    /// </summary>
    [ReactProp]
    public Action onPlay { get; set; }
    
    /// <summary>
    /// Called when media is paused
    /// </summary>
    [ReactProp]
    public Action onPause { get; set; }
    
    /// <summary>
    /// Called when media finishes playing
    /// <br/>
    /// Does not fire when loop is set to true
    /// </summary>
    [ReactProp]
    public Action onEnded { get; set; }
}