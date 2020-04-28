# WixNet35Bootstrapper
Shows an example Wix file that includes .Net 3.5 during installation

### Requirements
In some cases(Depending on the use of util:), you may need to use custom build command:
```
light -nologo -ext WixUIExtension -ext WixUtilExtension Installer.wixobj -o Installer.msi
```

### Code
```xml
<!-- depending on what components you want, you may need to add additional features to this command line -->
<CustomAction Id="AddNet35Component" Property="Net35Component" Value="&quot;[SystemFolder]dism.exe&quot; /norestart /quiet /online /enable-feature /featurename:NetFx3" Execute="immediate" />
<CustomAction Id="Net35Component" BinaryKey="WixCA" DllEntry="CAQuietExec" Execute="deferred" Return="ignore" Impersonate="no" />
<InstallExecuteSequence>
     <Custom Action="AddNet35Component" After="CostFinalize" />
     <Custom Action="Net35Component" After="InstallInitialize"><![CDATA[(NOT Installed)]]></Custom>
</InstallExecuteSequence>
<UI>
      <ProgressText Action="Net35Component">Enabling .Net 3.5</ProgressText>
</UI>
```

### Links
1. [wix example - Installing the IIS Web Server Role using DISM](https://gitlab.com/chris2/wix-toolset-examples/-/tree/master/InstallIISWithDISM)
2. [Quiet Execution Custom Action](https://wixtoolset.org/documentation/manual/v3/customactions/qtexec.html)
3. [WixEdit](https://wixedit.github.io/)