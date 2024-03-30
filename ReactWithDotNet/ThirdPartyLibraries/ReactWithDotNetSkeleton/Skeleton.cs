namespace ReactWithDotNet.ThirdPartyLibraries.ReactWithDotNetSkeleton;

public class Skeleton : PureComponent
{
    protected override Element render()
    {
        return new div
        {
            className = "skeleton",
            style =
            {
                BorderRadius(5),
                WidthFull,
                HeightFull
            },
            children =
            {
                new style
                {
                    @"

/*S K E L E T O N*/
@keyframes skeletonSweep {
    100% {
        transform: translateX(100%);
    }
}

.skeleton {
    position: relative;
    background: #DDDBDD;
    box-shadow: 0 1rem 1rem hsla(0, 0%, 0%, 0.05);
    border-radius: var(--border-radius-base);
    overflow: hidden;
}

.skeleton:after {
    display: block;
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    transform: translateX(-100%);
    background: linear-gradient(90deg, transparent, rgba(255, 255, 255, 0.3), transparent);
    animation: skeletonSweep 0.7s infinite;
}

"
                }
            }
        };
    }
}