using System;
using WixSharp;
using WixSharp.Bootstrapper;
using Assembly = System.Reflection.Assembly;

namespace Installer
{
    internal class Program
    {
        #region Properties

        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        #endregion

        #region Main

        private static void Main()
        {
            try
            {
                CreateMsi();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Methods

        private static void CreateMsi()
        {
            var project = new Project(
                "TestInstaller",
                new Dir(
                    @"%ProgramFiles%\TestCompany\TestInstaller",
                    new DirFiles(@"..\documents\*.*")
                )
            )
            {
                GUID = new Guid("815632CB-12E2-4C5D-94A3-88F03AEF54C8"),
                ControlPanelInfo =
                {
                    Manufacturer = "TestCompany",
                },
            };

            Compiler.WixLocation = Environment.ExpandEnvironmentVariables("%USERPROFILE%") +
                                   @"\.nuget\packages\WixSharp.wix.bin\3.11.2\tools\bin";
            var msi = Compiler.BuildMsi(project);

            CreateBootstrapperXp(msi).Build("TestInstaller.exe");
        }

        public static Bundle CreateBootstrapperXp(string msi)
        {
            var bootstrapper =
                new Bundle("TestInstaller", 
                    new ExePackage(@"..\documents\dotnetfx35setup.exe")
                    {
                        Name = "Microsoft .NET Framework 3.5",
                        InstallCommand = "/passive /norestart",
                        Permanent = true,
                        Vital = true,
                        DetectCondition = "NETFRAMEWORK35",
                        Compressed = true
                    },
                    new MsiPackage(msi))
                {
                    Version = Version,
                    UpgradeCode = new Guid("4FD2A66C-1EDD-4812-9AB2-A4A3A13768B8"),
                    Application =
                    {
                        AttributesDefinition = "ShowVersion=yes; ShowFilesInUse=yes; SuppressRepair=yes; SuppressOptionsUI=yes"
                    },
                };

            bootstrapper.Include(WixExtension.NetFx);

            return bootstrapper;
        }

        #endregion
    }
}
