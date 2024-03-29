using System.Diagnostics;
using System;
using System.Net;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading.Channels;

Console.Title = Environment.GetCommandLineArgs()[0] + " (Administrator)";
Console.BackgroundColor = ConsoleColor.Blue;
Console.Clear();

_ = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TM5"));
Console.Clear();
Console.WriteLine("Downloading TM5 and 7Zip . . .");

using (WebClient client = new())
{
    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

    client.DownloadFile("https://testmem.tz.ru/tm5.rar",
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TM5", "TM5.rar"));
    client.DownloadFile("https://www.7-zip.org/a/7z2301-x64.exe",
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TM5", "7-Zip.exe"));
}

// Create Config

string multilineComment = @"
Memory Test config file v0.02
Copyrights to the program belong to me.
Serj
testmem.tz.ru
serj_m@hotmail.com

[Main Section]
Config Name=ABSOLUT(01102021)
Config Author=anta777
Cores=0
Tests=16
Time (%)=1250
Cycles=3
Language=0
Test Sequence=1,4,6,15,3,2,7,15,5,2,8,15,4,2,9,15,3,2,10,15,5,2,11,15,4,2,12,15,5,14,15

[Global Memory Setup]
Channels=2
Interleave Type=1
Single DIMM width, bits=64
Operation Block, byts=64
Testing Window Size (Mb)=1536
Lock Memory Granularity (Mb)=64
Reserved Memory for Windows (Mb)=512
Capable=0x0
Debug Level=7

[Window Position]
WindowPosX=400
WindowPosY=400

[Test0]
Enable=1
Time (%)=8
Function=RefreshStable
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=0

[Test1]
Enable=1
Time (%)=240
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=2
Pattern Param0=0x77777777
Pattern Param1=0x33333333
Parameter=0
Test Block Size (Mb)=4

[Test2]
Enable=1
Time (%)=8
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=1
Pattern Param0=0
Pattern Param1=0
Parameter=0
Test Block Size (Mb)=0

[Test3]
Enable=1
Time (%)=20
Function=MirrorMove128
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=2
Test Block Size (Mb)=0

[Test4]
Enable=1
Time (%)=20
Function=MirrorMove
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=4
Test Block Size (Mb)=0

[Test5]
Enable=1
Time (%)=20
Function=MirrorMove128
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=1
Test Block Size (Mb)=0

[Test6]
Enable=1
Time (%)=240
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=4

[Test7]
Enable=1
Time (%)=120
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=8

[Test8]
Enable=1
Time (%)=60
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=16

[Test9]
Enable=1
Time (%)=30
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=32

[Test10]
Enable=1
Time (%)=16
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=64

[Test11]
Enable=1
Time (%)=8
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=128

[Test12]
Enable=1
Time (%)=8
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=256

[Test13]
Enable=1
Time (%)=8
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=512

[Test14]
Enable=1
Time (%)=8
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=0
Test Block Size (Mb)=0

[Test15]
Enable=1
Time (%)=8
Function=SimpleTest
DLL Name=bin\MT0.dll
Pattern Mode=0
Pattern Param0=0x0
Pattern Param1=0x0
Parameter=256
Test Block Size (Mb)=0
";

Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TM5", "7-Zip.exe"), "/S").WaitForExit();
Process.Start("C:\\Program Files\\7-Zip\\7z.exe",
    $"x \"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "tm5", "tm5.rar")}\" -o\"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "tm5")}\" -y");

File.WriteAllText(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads", "TM5", "TM5", "bin", "MT.cfg"), multilineComment);
File.SetAttributes(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads", "TM5", "TM5", "bin", "MT.cfg"), File.GetAttributes(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads", "TM5", "TM5", "bin", "MT.cfg")) | FileAttributes.ReadOnly);

File.SetAttributes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TM5", "TM5", "bin", "MT.cfg"), FileAttributes.ReadOnly);

_ = Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "TM5", "TM5", "TM5.exe"));