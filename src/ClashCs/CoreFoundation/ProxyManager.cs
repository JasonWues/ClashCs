﻿using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ClashCs.CoreFoundation;

public class ProxyManager
{
    private enum RET_ERRORS : int
    {
        RET_NO_ERROR = 0,
        INVALID_FORMAT = 1,
        NO_PERMISSION = 2,
        SYSCALL_FAILED = 3,
        NO_MEMORY = 4,
        INVAILD_OPTION_COUNT = 5,
    };
    
    private void SysproxyInvoke(string arguments)
    {
        if (OperatingSystem.IsWindows())
        {
            using AutoResetEvent outputWaitHandle = new AutoResetEvent(false);
            using AutoResetEvent errorWaitHandle = new AutoResetEvent(false);
            using Process process = new Process();
            //TODO add path
            process.StartInfo.FileName = "sysproxy.exe";
            process.StartInfo.Arguments = arguments;    
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            
            process.StartInfo.StandardOutputEncoding = Encoding.Unicode;
            process.StartInfo.StandardErrorEncoding = Encoding.Unicode;

            process.StartInfo.CreateNoWindow = true;
            
            StringBuilder output = new StringBuilder();
            StringBuilder error = new StringBuilder();

            process.OutputDataReceived += (sender, args) =>
            {
                if (args.Data == null)
                {
                    outputWaitHandle.Set();
                }
                else
                {
                    output.AppendLine(args.Data);
                }

            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data == null)
                {
                    errorWaitHandle.Set();
                }
                else
                {
                    error.AppendLine(args.Data);
                }
            };

            try
            {
                process.Start();
                
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            string stderr = error.ToString();
            string stdout = output.ToString();
            
            int exitCode = process.ExitCode;
            if (exitCode != (int)RET_ERRORS.RET_NO_ERROR)
            {
                throw new Exception(stderr);
            }
        }
        else if (OperatingSystem.IsLinux())
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };
            process.Start();
            
            //TODO set proxy
            process.StandardInput.WriteLine("");
        }
        
    }
}