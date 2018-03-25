using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Helpers
{
    public interface IFormOpenerHelper
    {
        void ShowModelessForm<TForm>() where TForm : Form;
        DialogResult ShowModalForm<TForm>() where TForm : Form;
        TForm GetForm<TForm>() where TForm : Form;
        bool IsOpen<TForm>() where TForm : Form;
        void CloseForm<TForm>() where TForm : Form;
    }

    /// <summary>
    /// SimpleInjector FormOpener helper.
    /// https://stackoverflow.com/questions/38417654/winforms-how-to-register-forms-with-ioc-container/38421425#38421425
    /// </summary>
    public class FormOpenerHelper : IFormOpenerHelper
    {
        private readonly Container container;
        private readonly Dictionary<Type, Form> openedForms;

        public FormOpenerHelper(Container container)
        {
            this.container = container;
            openedForms = new Dictionary<Type, Form>();
        }

        public void ShowModelessForm<TForm>() where TForm : Form
        {
            Form form;

            if (openedForms.ContainsKey(typeof(TForm)))
            {
                //A form can be held open in the background, somewhat like singleton behavior, and reopened/re-shown this way when a form is 'closed' using form.Hide()   
                form = openedForms[typeof(TForm)];
            }
            else
            {
                form = GetFormFormContainer<TForm>();

                openedForms.Add(form.GetType(), form);
                //The form will be closed and disposed when form.Closed is called remove it from the cached instances so it can be recreated

                form.Closed += (s, e) => openedForms.Remove(form.GetType());
            }

            form.Show();
        }

        public DialogResult ShowModalForm<TForm>() where TForm : Form
        {
            using (var form = GetFormFormContainer<TForm>())
            {
                return form.ShowDialog();
            }
        }
        public TForm GetForm<TForm>() where TForm : Form
        {
            if (openedForms.ContainsKey(typeof(TForm)))
            {
                return (TForm)openedForms[typeof(TForm)];
            }

            throw new InvalidOperationException($"Form {typeof(TForm).ToString()} has not been opened");
        }

        public bool IsOpen<TForm>() where TForm : Form
        {
            return openedForms.ContainsKey(typeof(TForm));
        }

        public void CloseForm<TForm>() where TForm : Form
        {
            if (openedForms.ContainsKey(typeof(TForm)))
            {
                openedForms[typeof(TForm)].Close();
            }
        }

        private TForm GetFormFormContainer<TForm>() where TForm : Form
        {
            return container.GetInstance<TForm>();
        }
    }
}
