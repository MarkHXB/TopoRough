using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopoRough.Events;

namespace TopoRough.Models
{
    public interface IFormHandler
    {
        void SwitchForm(Form currentForm, object newForm,string switchToName);
    }
    public class FormHandler:IFormHandler
    {
        public Form PreviousPage { get; set; }

        public void SwitchForm(Form currentForm, object newForm,string switchToName)
        {
            currentForm.Hide();
 
            try
            {
                if (PreviousPage != null)
                {
                    Form form = Application.OpenForms[PreviousPage.Name];
                    form.Show();
                }
                else
                {
                    Form form = (Form)newForm;
                    if (form is not null)
                    {
                        form.Show();
                    }
                }

                GlobalVariables.ActiveMenuSection = $"{switchToName}";
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            finally
            {
                PreviousPage = currentForm;
                GlobalVariables.LoadingIsValid = true;
            }
        }
        public void Exit_On_Save(Panel sandboxPanel)
        {
            DialogResult dialogResult = MessageBox.Show("Igen", "Szeretnéd menteni a jelenlegi munkádat?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                EventsHandler.Save_Work(sandboxPanel);
                EventsHandler.Save_User_Settings(sandboxPanel);
            }
            else if (dialogResult == DialogResult.No)
            {
                EventsHandler.Save_User_Settings(sandboxPanel);
            }
        }
    }
}
