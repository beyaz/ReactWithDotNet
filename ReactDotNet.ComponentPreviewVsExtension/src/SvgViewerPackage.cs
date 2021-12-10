using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using PuppeteerSharp;
using Task = System.Threading.Tasks.Task;




namespace SvgViewer
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [Guid("058ea1e2-7ae5-486c-8888-ee1d7cc2b49b")]
    public sealed class SvgViewerPackage : AsyncPackage
    {
    }
}
