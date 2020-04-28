# WixNet35Bootstrapper
Shows an example Wix file that includes .Net 3.5 during installation

### Requirements
In some cases, you may need to use custom build command:
```
light -nologo -ext WixUIExtension -ext WixUtilExtension Installer.wixobj -o Installer.msi
```

### Links
1. [wix example - Installing the IIS Web Server Role using DISM](https://gitlab.com/chris2/wix-toolset-examples/-/tree/master/InstallIISWithDISM)
2. [Quiet Execution Custom Action](https://wixtoolset.org/documentation/manual/v3/customactions/qtexec.html)
3. [WixEdit](https://wixedit.github.io/)