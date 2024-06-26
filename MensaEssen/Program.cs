using System;
using System.Drawing;
using System.Windows.Forms;

namespace MensaEssen
{
    internal static class Program
    {
        private static System.Windows.Forms.Timer minimizeTimer = new System.Windows.Forms.Timer();

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            MensaEssen form = new MensaEssen()
            {
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false
            };

            Screen? primaryScreen = Screen.PrimaryScreen;
            if (primaryScreen != null)
            {
                int screenWidth = primaryScreen.WorkingArea.Width;
                int screenHeight = primaryScreen.WorkingArea.Height;
                int formWidth = form.Width;
                int formHeight = form.Height;
                int x = screenWidth - formWidth;
                int y = screenHeight - formHeight;
                form.Location = new Point(x, y);
            }

            // Setze den Initialzustand auf minimiert
            form.WindowState = FormWindowState.Minimized;

            // Erstelle ein NotifyIcon-Steuerelement
            Icon? projectIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = projectIcon,
                Visible = true
            };

            // Erstelle ein Kontextmenü für das NotifyIcon
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem minimizeItem = new ToolStripMenuItem("Minimieren");
            minimizeItem.Click += (sender, e) =>
            {
                form.Hide();  // Verstecke das Formular anstatt es zu minimieren
            };
            contextMenu.Items.Add(minimizeItem);

            ToolStripMenuItem restoreItem = new ToolStripMenuItem("Maximieren");
            restoreItem.Click += (sender, e) =>
            {
                form.Show();  // Zeige das Formular wieder an
                form.WindowState = FormWindowState.Normal;
            };
            contextMenu.Items.Add(restoreItem);

            ToolStripMenuItem closeItem = new ToolStripMenuItem("Schließen");
            closeItem.Click += (sender, e) =>
            {
                form.Close();  // Schließe das Formular 
            };
            contextMenu.Items.Add(closeItem);

            notifyIcon.ContextMenuStrip = contextMenu;

            // Behandele das MouseClick-Ereignis des NotifyIcon, um das Kontextmenü anzuzeigen
            notifyIcon.MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenu.Show(Cursor.Position);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (form.Visible)
                    {
                        form.Hide();  // Verstecke das Formular anstatt es zu minimieren
                    }
                    else if (!form.Visible)
                    {
                        form.Show();  // Zeige das Formular wieder an
                        form.WindowState = FormWindowState.Normal;
                    }
                }
            };

            // Minimiere das Formular sofort, wenn es den Fokus verliert
            form.Deactivate += (sender, e) =>
            {
                form.Hide();  // Verstecke das Formular anstatt es zu minimieren
            };

            // Minimiere das Formular, wenn der Mauszeiger das Formular verlässt
            form.MouseLeave += (sender, e) =>
            {
                // Start a short delay to see if the mouse enters again quickly
                minimizeTimer.Interval = 1000; // 1 second delay
                minimizeTimer.Tick += (s, args) =>
                {
                    minimizeTimer.Stop();
                    if (!form.Bounds.Contains(Cursor.Position))
                    {
                        form.Hide();  // Verstecke das Formular anstatt es zu minimieren
                    }
                };
                minimizeTimer.Start();
            };

            // Stop the timer if the mouse re-enters the form
            form.MouseEnter += (sender, e) =>
            {
                minimizeTimer.Stop();
            };

            Application.Run(form);
        }
    }
}
