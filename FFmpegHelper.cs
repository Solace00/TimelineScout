using System;
using System.Diagnostics;

namespace TimelineScout
{
    internal static class FFmpegHelper
    {
        public static void RunFFmpegCommand(string arguments)
        {
            // Define the path to the FFmpeg executable
            string ffmpegPath = @"C:\FFmpeg\bin\ffmpeg.exe"; // Update this to your actual path

            // Add debugging output
            Console.WriteLine($"Executing FFmpeg command: {ffmpegPath} {arguments}");

            // Create a process to run the FFmpeg command
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.OutputDataReceived += (sender, e) => Console.WriteLine($"FFmpeg Output: {e.Data}");
                process.ErrorDataReceived += (sender, e) => Console.WriteLine($"FFmpeg Error: {e.Data}");

                // Start the process and begin reading the output
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Wait for the process to exit
                process.WaitForExit();

                // Log the exit code
                Console.WriteLine($"FFmpeg process exited with code: {process.ExitCode}");
            }
        }

        public static void PlayVideo(string filePath)
        {
            // Example FFmpeg command to play a video in a window
            string ffmpegArguments = $"-i \"{filePath}\" -vf \"scale=640:360\" -f sdl2 \"Video Player\"";
            RunFFmpegCommand(ffmpegArguments);
        }
    }
}