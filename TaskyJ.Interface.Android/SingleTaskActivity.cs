using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskyJ.Business;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Interface.Android
{
    [Activity(Label = "SingleTask")]
    public class SingleTaskActivity : Activity
    {
        DBTaskJ internaltask = null;
        //TODO: use sessions
        DBSessionJ CurrentSession = new DBSessionJ();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CurrentSession = TaskyJManager.Authenticate("admin", "admin", "127.0.0.1");

            SetContentView(TaskyJ.Interface.Android.Resource.Layout.SingleTask);

            Button cmdSave = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdSave);
            cmdSave.Click += delegate
            {
                SaveRecord();
            };
            Button cmdDelete = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdDelete);
            cmdDelete.Click += delegate
            {
                DeleteRecord();
            };
            Button cmdCancel = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdCancel);
            cmdCancel.Click += delegate
            {
                Finish();
            };

            int initialid = 0;
            try
            {
                initialid = Intent.GetIntExtra("initialid", 0);
            }
            catch { }

            NumberPicker completed = FindViewById<NumberPicker>(TaskyJ.Interface.Android.Resource.Id.nbpCompleted);
            completed.MaxValue = 100;
            completed.MinValue = 0;
            completed.Value = 0;

            Spinner priority = FindViewById<Spinner>(TaskyJ.Interface.Android.Resource.Id.spnPriority);
            List<DBTaskJ.TaskPriority> lstPrio = new List<DBTaskJ.TaskPriority>();
            foreach (DBTaskJ.TaskPriority item in Enum.GetValues(typeof(DBTaskJ.TaskPriority)))
            {
                lstPrio.Add(item);
            }
            ArrayAdapter<DBTaskJ.TaskPriority> priorityItems = new ArrayAdapter<DBTaskJ.TaskPriority>(this, global::Android.Resource.Layout.SimpleListItem1, lstPrio.ToArray());
            priorityItems.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            priority.Adapter = priorityItems;

            DatePicker pckDate = FindViewById<DatePicker>(TaskyJ.Interface.Android.Resource.Id.pckDate);
            TimePicker pckTime = FindViewById<TimePicker>(TaskyJ.Interface.Android.Resource.Id.pckTime);

            CheckBox chkSpecifyDeadline = FindViewById<CheckBox>(TaskyJ.Interface.Android.Resource.Id.chkSpecifyDeadline);
            chkSpecifyDeadline.Click += (o, e) =>
            {
                if (chkSpecifyDeadline.Checked)
                    pckDate.Visibility = pckTime.Visibility = global::Android.Views.ViewStates.Visible;
                else
                    pckDate.Visibility = pckTime.Visibility = global::Android.Views.ViewStates.Gone;
            };

            Spinner category = FindViewById<Spinner>(TaskyJ.Interface.Android.Resource.Id.spnCategory);
            var lstCats = Business.TaskyJManager.RetrieveCategories().ToList();
            lstCats.Insert(0, new DBCategoryJ { ID = -1, Name = "<<No Category>>" });

            var categoryItems = new ArrayAdapter<DBCategoryJ>(this, global::Android.Resource.Layout.SimpleListItem1, lstCats.ToArray());
            categoryItems.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            category.Adapter = categoryItems;

            if (initialid != 0)
            {
                internaltask = Business.TaskyJManager.GetTaskById(initialid);
                EditText id = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.ID);
                id.Text = internaltask.ID.ToString();
                EditText name = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.Name);
                name.Text = internaltask.Name;
                EditText description = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.Description);
                description.Text = internaltask.Description;
                ImageView ivwCategory = FindViewById<ImageView>(TaskyJ.Interface.Android.Resource.Id.ivwCategory);
                completed.Value = internaltask.Completed;
                priority.SetSelection((byte)internaltask.Priority);
                if (internaltask.Category != null)
                {
                    int currPosCat = 0;
                    while (currPosCat < category.Count)
                    {
                        Java.Lang.Object obj = category.GetItemAtPosition(currPosCat);
                        var propertyInfo = obj.GetType().GetProperty("Instance");
                        DBCategoryJ currCat = (propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as DBCategoryJ);
                        if (currCat.ID == internaltask.Category.ID)
                        {
                            category.SetSelection(currPosCat);
                            byte[] data = Convert.FromBase64String(internaltask.Category.IconBase64);
                            Bitmap bmp = BitmapFactory.DecodeByteArray(data, 0, data.Length);
                            ivwCategory.SetImageBitmap(bmp);
                            break;
                        }
                        currPosCat++;
                    }
                }
                else
                {
                    category.SetSelection(0);
                }

                EditText creationdate = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.CreationDate);
                creationdate.Text = internaltask.CreationDate.ToString();

                if (internaltask.Deadline != null)
                {
                    chkSpecifyDeadline.Checked = true;
                    pckDate.Visibility = pckTime.Visibility = global::Android.Views.ViewStates.Visible;
                    pckDate.DateTime = internaltask.Deadline.Value.Date;
                    pckTime.Hour = internaltask.Deadline.Value.Hour;
                    pckTime.Minute = internaltask.Deadline.Value.Minute;
                }

                if (internaltask.Deleted)
                {
                    cmdDelete.Text = "Restore";
                    cmdSave.Enabled = false;
                }
            }
            else
            {
                EditText creationdate = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.CreationDate);
                TextView creationdatelabel = FindViewById<TextView>(TaskyJ.Interface.Android.Resource.Id.CreationDateLabel);
                creationdate.Visibility = creationdatelabel.Visibility = global::Android.Views.ViewStates.Gone;
            }
        }

        public override void OnBackPressed()
        {
            if (internaltask != null)
            {
                if (!internaltask.Equals(GetTask()))
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Exit");
                    builder.SetMessage("Data has not been saved, are you sure you want to exit?");
                    builder.SetIcon(global::Android.Resource.Drawable.IcDialogAlert);
                    builder.SetPositiveButton(global::Android.Resource.String.Yes, delegate (object sender, DialogClickEventArgs e)
                    {
                        Finish();
                    });
                    builder.SetNegativeButton(global::Android.Resource.String.No, delegate (object sender, DialogClickEventArgs e) { });
                    builder.Show();
                }
                else
                {
                    Finish();
                }
            }
            else
            {
                Finish();
            }
        }

        private DBTaskJ GetTask()
        {
            EditText id = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.ID);
            EditText name = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.Name);
            EditText description = FindViewById<EditText>(TaskyJ.Interface.Android.Resource.Id.Description);
            NumberPicker completed = FindViewById<NumberPicker>(TaskyJ.Interface.Android.Resource.Id.nbpCompleted);
            Spinner priority = FindViewById<Spinner>(TaskyJ.Interface.Android.Resource.Id.spnPriority);
            CheckBox chkSpecifyDeadline = FindViewById<CheckBox>(TaskyJ.Interface.Android.Resource.Id.chkSpecifyDeadline);
            DatePicker pckDate = FindViewById<DatePicker>(TaskyJ.Interface.Android.Resource.Id.pckDate);
            TimePicker pckTime = FindViewById<TimePicker>(TaskyJ.Interface.Android.Resource.Id.pckTime);
            Spinner category = FindViewById<Spinner>(TaskyJ.Interface.Android.Resource.Id.spnCategory);

            var t = new DBTaskJ();
            t.ID = 0;
            if (!string.IsNullOrEmpty(id.Text))
                t.ID = Convert.ToInt32(id.Text);
            t.Name = name.Text;
            t.Description = description.Text;
            t.SetPriority(priority.SelectedItemPosition);
            try
            {
                t.Completed = Convert.ToByte(completed.Value);
            }
            catch
            {
                t.Completed = 0;
            }
            if (internaltask != null)
            {
                t.CreationDate = internaltask.CreationDate;
                t.Deleted = internaltask.Deleted;
                t.FinishDate = internaltask.FinishDate;
            }
            if (chkSpecifyDeadline.Checked)
                t.Deadline = pckDate.DateTime.AddHours(pckTime.Hour).AddMinutes(pckTime.Minute);
            else
                t.Deadline = null;
            if (category.SelectedItem != null)
            {
                if (category.SelectedItemPosition == 0)
                {
                    t.Category = null;
                }
                else
                {
                    Java.Lang.Object obj = category.GetItemAtPosition(category.SelectedItemPosition);
                    var propertyInfo = obj.GetType().GetProperty("Instance");
                    t.Category = (propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as DBCategoryJ);
                }
            }
            return t;
        }

        public void DeleteRecord()
        {
            var tsk = GetTask();
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            if (tsk.Deleted)
                builder.SetTitle("Restore task");
            else
                builder.SetTitle("Delete task");
            builder.SetMessage("Are you sure?");
            builder.SetIcon(global::Android.Resource.Drawable.IcDialogAlert);
            builder.SetPositiveButton(global::Android.Resource.String.Yes, delegate (object sender, DialogClickEventArgs e)
                {
                    if (tsk.Deleted)
                    {
                        tsk.Deleted = false;
                        Business.TaskyJManager.PushTask(tsk, CurrentSession);
                        Toast.MakeText(ApplicationContext, "Restored", ToastLength.Short).Show();
                    }
                    else
                    {
                        Business.TaskyJManager.RemoveTask(tsk);
                        Toast.MakeText(ApplicationContext, "Deleted", ToastLength.Short).Show();
                    }
                    NotifyMain();
                    Finish();
                });
            builder.SetNegativeButton(global::Android.Resource.String.No, delegate (object sender, DialogClickEventArgs e) { });
            builder.Show();
        }

        public void SaveRecord()
        {
            Business.TaskyJManager.PushTask(GetTask(), CurrentSession);
            Toast.MakeText(ApplicationContext, "Saved", ToastLength.Short).Show();
            NotifyMain();
            Finish();
        }

        private void NotifyMain()
        {
            Intent myIntent = new Intent(this, typeof(MainActivity));
            SetResult(Result.Ok, myIntent);
        }

    }

}
