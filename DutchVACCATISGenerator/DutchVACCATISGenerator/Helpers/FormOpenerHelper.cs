using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Helpers
{
    public interface IFormOpenerHelper
    {
        /// <summary>
        /// Open modeless form.
        /// </summary>
        /// <typeparam name="TForm">Type of form</typeparam>
        void ShowModelessForm<TForm>() where TForm : Form;

        /// <summary>
        /// Open modal form.
        /// </summary>
        /// <typeparam name="TForm">Type of form</typeparam>
        /// <returns>Dialog result of opened modal</returns>
        DialogResult ShowModalForm<TForm>() where TForm : Form;

        /// <summary>
        /// Gets form from the container.
        /// </summary>
        /// <typeparam name="TForm">Type of form</typeparam>
        /// <returns>Type of requested form</returns>
        TForm GetForm<TForm>() where TForm : Form;

        /// <summary>
        /// Indicates if a form is open.
        /// </summary>
        /// <typeparam name="TForm">Type of form</typeparam>
        /// <returns>True if open, else false</returns>
        bool IsOpen<TForm>() where TForm : Form;

        /// <summary>
        /// Closes a form.
        /// </summary>
        /// <typeparam name="TForm">Type of form</typeparam>
        void CloseForm<TForm>() where TForm : Form;

        /// <summary>
        /// Show a form.
        /// </summary>
        /// <typeparam name="TForm">Type of form</typeparam>
        void Show<TForm>() where TForm : Form;

        /// <summary>
        /// Hides a form.
        /// </summary>
        /// <typeparam name="TForm">Type of form</typeparam>
        void Hide<TForm>() where TForm : Form;
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
            using (var form = this.GetFormFormContainer<TForm>())
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

        public void Show<TForm>() where TForm : Form
        {
            if (openedForms.ContainsKey(typeof(TForm)))
            {
                openedForms[typeof(TForm)].Show();
            }
        }

        public void Hide<TForm>() where TForm : Form
        {
            if (openedForms.ContainsKey(typeof(TForm)))
            {
                openedForms[typeof(TForm)].Hide();
            }
        }

        private TForm GetFormFormContainer<TForm>() where TForm : Form
        {
            return container.GetInstance<TForm>();
        }
    }
}
