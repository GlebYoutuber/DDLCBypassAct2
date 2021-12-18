using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;

namespace DDLCBypassAct2
{
    public partial class DDLCBypass : Form
    {
        public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            //DON'T TOUCH THIS CODE!!!

            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }
        public DDLCBypass()
        {
            InitializeComponent();
            Directory.CreateDirectory(@"C:\Bypass");
            foreach(var killddlcifgameisstarted in Process.GetProcessesByName("DDLC"))
            {
                killddlcifgameisstarted.Kill();
                MessageBox.Show("DDLC Process is Successfully Killed", "DDLC Bypass Act 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (MessageBox.Show("Do you want to delete FirstRun?", "DDLC Bypass Act 2", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes) 
            {
                string ddlcsteam = @"C:\Program Files (x86)\Steam\steamapps\common\Doki Doki Literature Club";
                DirectoryInfo ddlcxzx = new DirectoryInfo(ddlcsteam + @"\game");
                FileInfo[] fileInfos = ddlcxzx.GetFiles("firstrun");
                foreach (FileInfo DDLCsteamdeletesaves in fileInfos)
                {
                    DDLCsteamdeletesaves.Attributes = FileAttributes.Normal;
                    DDLCsteamdeletesaves.Delete();
                }
                MessageBox.Show("Done");
            }
            if(MessageBox.Show("Do you want restore .chr files?", "DDLC Bypass Act 2", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                File.Delete(@"C:\Program Files (x86)\Steam\steamapps\common\Doki Doki Literature Club\characters\yuri.chr");
                File.Delete(@"C:\Program Files (x86)\Steam\steamapps\common\Doki Doki Literature Club\characters\natsuki.chr");
                Extract("DDLCBypassAct2", @"C:\Bypass", "Resources", "yuri.chr");
                Extract("DDLCBypassAct2", @"C:\Bypass", "Resources", "sayori.chr");
                Extract("DDLCBypassAct2", @"C:\Bypass", "Resources", "natsuki.chr");
                File.Copy(@"C:\Bypass\yuri.chr", @"C:\Program Files (x86)\Steam\steamapps\common\Doki Doki Literature Club\characters\yuri.chr");
                File.Copy(@"C:\Bypass\sayori.chr", @"C:\Program Files (x86)\Steam\steamapps\common\Doki Doki Literature Club\characters\sayori.chr");
                File.Copy(@"C:\Bypass\natsuki.chr", @"C:\Program Files (x86)\Steam\steamapps\common\Doki Doki Literature Club\characters\natsuki.chr");
                Directory.Delete(@"C:\Bypass");
                MessageBox.Show("Done");
            }
            if (MessageBox.Show("Do you want to delete DDLC's save files from Renpy Saves Folder?", "DDLC Bypass Act 2", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string renpyddlcsavesfolder = $@"C:\Documents and Settings\{Environment.UserName}\Application Data\RenPy\DDLC-1454445547";
                DirectoryInfo renpyddlc = new DirectoryInfo(renpyddlcsavesfolder);
                FileInfo[] ddlcrenpysavesfolder = renpyddlc.GetFiles("*.save");
                foreach(FileInfo renppy in ddlcrenpysavesfolder)
                {
                    renppy.Delete();
                }
                FileInfo[] dokidokipers = renpyddlc.GetFiles("persistent");
                foreach (FileInfo ddlcxver in dokidokipers)
                {
                    ddlcxver.Delete();
                }
                MessageBox.Show("Done");
            }
            MessageBox.Show("DDLC Act 2 is Successfully Bypassed" + Environment.NewLine + "Creator of this bypass is GlebYoutuber", "DDLC Bypass Act 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Process.Start(@"C:\Program Files (x86)\Steam\steamapps\common\Doki Doki Literature Club\DDLC.exe");
            Environment.Exit(75021);
        }

    }
}
