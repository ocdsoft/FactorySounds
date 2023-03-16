// See https://aka.ms/new-console-template for more information
// VLC code that will work cross-platform borrowed from https://stackoverflow.com/questions/42845506/how-to-play-a-sound-in-netcore

using System.Diagnostics;
using System.Runtime.InteropServices;

string path = Environment.GetCommandLineArgs()[0];
string dllName = Path.GetFileName(path);
string audioFile = Environment.GetCommandLineArgs()[1];
Play(path.Replace(dllName, "") + audioFile);


static void Play(string path)
{

    var keyPressed = Console.ReadKey(true);

    if (keyPressed.Key != ConsoleKey.Escape)
    {

        string program = "vlc.exe";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            program = "cvlc";

        var pi = new ProcessStartInfo(path)
        {
            Arguments = Path.GetFileName(path) + " --play-and-exit",
            UseShellExecute = true,
            WorkingDirectory = Path.GetDirectoryName(path),
            FileName = program,
            Verb = "OPEN",
            WindowStyle = ProcessWindowStyle.Hidden
        };

        Process p = new Process();
        p.StartInfo = pi;
        p.Start();
        Console.WriteLine("Play sound...");
        p.WaitForExit();
        Console.WriteLine("");
        string? readline = string.Empty;

        if (Console.KeyAvailable)
        {
            Console.WriteLine("Clear additional keystrokes");
            Console.WriteLine("");
        }

        while (Console.KeyAvailable) 
        { 
            Console.ReadKey(true);  
        }

        Play(path);
    }

}

