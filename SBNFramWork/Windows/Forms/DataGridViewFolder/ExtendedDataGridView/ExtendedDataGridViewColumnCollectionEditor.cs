﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Reflection;

namespace Sbn.FramWork.Windows.Forms.ExtendedDataGridView
{
    class ExtendedDataGridViewColumnCollectionEditor : UITypeEditor
    {
        Form m_dlgColumnEditor;

        ExtendedDataGridViewColumnCollectionEditor()
        {
        }

        public static Form CreateColumnEditor()
        {
            Type columnEditorType = Assembly.Load("System.Design").GetType("System.Windows.Forms.Design.DataGridViewColumnCollectionDialog");

            var vv = columnEditorType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
           

            var constructor = columnEditorType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];

             //MessageBox.Show("constructor.Name1 : " + constructor.Name + "\n"
             //    + constructor.GetParameters()[0].ToString() );

            var form = (Form)constructor.Invoke(new object[1]);

           
            form.MaximizeBox = true;
            form.Size = new System.Drawing.Size(10, 10);

            form.Load += new EventHandler(form_Load);

            return form;
        }

        static void form_Load(object sender, EventArgs e)
        {
            LoadData((Form)sender);
        }

        public static void SetGrid(Form form, DataGridView grid)
        {
            form.GetType().GetMethod("SetLiveDataGridView", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(form, new object[] { grid });
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                var service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (service == null || context.Instance == null)
                    return value;

                var host = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
                if (host == null)
                    return value;

                if (m_dlgColumnEditor == null)
                    m_dlgColumnEditor = CreateColumnEditor();

                SetGrid(m_dlgColumnEditor, (DataGridView)context.Instance);
                
                using (DesignerTransaction transaction = host.CreateTransaction("DataGridViewColumnCollectionTransaction"))
                {
                    if (service.ShowDialog(m_dlgColumnEditor) == DialogResult.OK)
                    {
                       // transaction.Commit();
                      
                    }
                    else
                        transaction.Cancel();
                }
            }

            SaveData(m_dlgColumnEditor);
            return value;
        }

        public static void SaveData(Form form)
        {
            //var properties = Properties.Settings.Default;
            //properties.IsColEditorMaximised = form.WindowState == FormWindowState.Maximized;
            //properties.ColEditorSize = (form.WindowState == FormWindowState.Normal) ? form.Size : form.RestoreBounds.Size;
            //properties.ColEditorLocation = (form.WindowState == FormWindowState.Normal) ? form.Location : form.RestoreBounds.Location;
            //properties.Save();
        }

        static void LoadData(Form form)
        {
            //form.Size        = Properties.Settings.Default.ColEditorSize;
            //form.Location    = Properties.Settings.Default.ColEditorLocation;
            //form.WindowState = Properties.Settings.Default.IsColEditorMaximised ? FormWindowState.Maximized
            //                                                                    : FormWindowState.Normal;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
