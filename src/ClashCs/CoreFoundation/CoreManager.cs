using System;
using System.Diagnostics;
using System.Text;
using ClashCs.Config;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public class CoreManager
{
    private Process process;

    public CoreManager()
    {
        
    }

    public void LoadCore(LocalConfig localConfig)
    {
        if (localConfig.EnableTun && !Util.Instance.Value.IsAdministrator())
        {
            
        }
    }

    public void CoreStart()
    {
        try
        {
            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    //FileName = fileName,
                    //Arguments = arguments,
                    //WorkingDirectory = Utils.GetConfigPath(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                }
            };

            p.OutputDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    string msg = args.Data + Environment.NewLine;
                    //TODO 转发到日志
                }
            };

            p.Start();
            p.BeginOutputReadLine();
            process = p;
            
            if (p.WaitForExit(1000))
            {
                throw new Exception(p.StandardError.ReadToEnd());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CoreStop()
    {
        try
        {
            if (process != null)
            {
                process.Dispose();
                process = null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private void KillProcess(Process p)
    {
        try
        {
            p.CloseMainWindow();
            p.WaitForExit(100);
            if (p.HasExited)
            {
                return;
            }
            p.Kill();
            p.WaitForExit(100);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}