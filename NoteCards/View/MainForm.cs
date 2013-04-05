using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NoteCards.DomainModel;

namespace NoteCards.View
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public event EventHandler NewNote;
        public event EventHandler NoteSelected;
        public event EventHandler SaveNote;
        public event EventHandler DeleteNote;

        public string SelectedNote
        {
            get
            {
                return noteList.SelectedItems[0].Text;
            }
            set { noteList.Items[value].Selected = true; }
        }

        public string StatusText
        {
            get { return statusLabel.Text; }
            set { statusLabel.Text = value; }
        }

        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        public void DisplayMessage(string title, string message)
        {
            MessageBox.Show(message, title);
        }

        public void ClearSelectedNote()
        {
            noteList.SelectedItems.Clear();
        }

        public void SetSelectedNote(string title)
        {
            noteList.Items[title].Selected = true;
        }

        public void LoadNotes(IEnumerable<string> notes)
        {
            //the listView is not bindable, so we have to re-populate when the underlying collection change
            noteList.Items.Clear();
            noteList.Items.AddRange(notes.ToList().Select(note => new ListViewItem(note){Name = note}).ToArray());
        }

        public void LoadNote(NoteCard note)
        {
            titleText.Text = note.Title;
            noteText.Text = note.NoteText;
            createdText.Text = String.Format("{0}", note.CreateDate);
            editedText.Text = String.Format("{0}", note.EditDate);
        }

        public NoteCard GetNote()
        {
            return new NoteCard
            {
                Title = titleText.Text,
                NoteText = noteText.Text
            };
        }

        private void noteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noteList.SelectedItems.Count>0 && NoteSelected != null)
                NoteSelected(sender, e);
        }

        private void newNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NewNote != null)
                NewNote(sender, e);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (SaveNote != null)
                SaveNote(sender, e);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (DeleteNote != null)
                DeleteNote(sender, e);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
