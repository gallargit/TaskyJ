using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;
using static Android.Widget.AdapterView;

namespace TaskyJ.Interface.Android
{
    [Activity(Label = "TaskyJ", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //ConfigurationManager.AppSettings does not work on Android - this is hardcoded by now
        //private string database = "sqlite";
        //TODO: use sessions
        DBSessionJ CurrentSession = new DBSessionJ();

        //private string database = "mongodb";
        private string database = "stsdb";

        private static int EDIT_TASK = 10;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                RefreshList();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TaskyJManager.SetRepoTask(Globals.EngineAndroid.ResolveRepo(database));
            TaskyJManager.SetRepoCat(Globals.EngineAndroid.ResolveRepoCat(database));
            TaskyJManager.SetRepoUsr(Globals.EngineAndroid.ResolveRepoUsr(database));

            //CurrentSession = TaskyJManager.Authenticate("admin", "admin", "127.0.0.1");

            //do not remove "TaskyJ.Interface.Android." before "Resource."
            SetContentView(TaskyJ.Interface.Android.Resource.Layout.Main);

            ListView lv = FindViewById<ListView>(TaskyJ.Interface.Android.Resource.Id.TaskListView);
            lv.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                DBTaskJ tsk = ((ArrayAdapter<DBTaskJ>)lv.Adapter).GetItem(e.Position);
                if (tsk.ID > 0)
                {
                    Intent intent = new Intent(this, typeof(SingleTaskActivity));
                    intent.PutExtra("initialid", tsk.ID);
                    StartActivityForResult(intent, EDIT_TASK);
                }
            };
            lv.LongClickable = true;
            lv.ItemLongClick += (object sender, ItemLongClickEventArgs e) =>
            {
                DBTaskJ tsk = ((ArrayAdapter<DBTaskJ>)lv.Adapter).GetItem(e.Position);
                if (tsk.ID > 0)
                {
                    var menu = new PopupMenu(this, (View)sender);
                    menu.Inflate(TaskyJ.Interface.Android.Resource.Layout.taskmenu);
                    if (tsk.Completed >= 100)
                    {
                        menu.Menu.FindItem(TaskyJ.Interface.Android.Resource.Id.menuFinish).SetVisible(false);
                    }
                    if (tsk.Deleted)
                    {
                        menu.Menu.FindItem(TaskyJ.Interface.Android.Resource.Id.menuDelete).SetVisible(false);
                        menu.Menu.FindItem(TaskyJ.Interface.Android.Resource.Id.menuFinish).SetVisible(false);
                    }
                    else
                    {
                        menu.Menu.FindItem(TaskyJ.Interface.Android.Resource.Id.menuRestore).SetVisible(false);
                    }
                    menu.MenuItemClick += (s, a) =>
                    {
                        switch (a.Item.ItemId)
                        {
                            case TaskyJ.Interface.Android.Resource.Id.menuFinish:
                                tsk.Completed = 100;
                                tsk.CheckFinishTask();
                                TaskyJManager.PushTask(tsk, CurrentSession);
                                RefreshList();
                                Toast.MakeText(ApplicationContext, "Finished", ToastLength.Short).Show();
                                break;
                            case TaskyJ.Interface.Android.Resource.Id.menuDelete:
                                TaskyJManager.RemoveTask(tsk);
                                RefreshList();
                                Toast.MakeText(ApplicationContext, "Deleted", ToastLength.Short).Show();
                                break;
                            case TaskyJ.Interface.Android.Resource.Id.menuRestore:
                                tsk.Deleted = false;
                                TaskyJManager.PushTask(tsk, CurrentSession);
                                RefreshList();
                                Toast.MakeText(ApplicationContext, "Restored", ToastLength.Short).Show();
                                break;
                        }
                    };
                    menu.Show();
                }
            };

            Button cmdAddNew = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdAddNew);
            cmdAddNew.Click += delegate
            {
                Intent intent = new Intent(this, typeof(SingleTaskActivity));
                intent.PutExtra("initialid", 0);
                StartActivityForResult(intent, EDIT_TASK);
            };

            Button cmdQuickAdd = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdQuickAdd);
            cmdQuickAdd.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Enter task name");
                EditText input = new EditText(this);
                input.InputType = global::Android.Text.InputTypes.ClassText;
                builder.SetView(input);
                builder.SetPositiveButton(global::Android.Resource.String.Ok, delegate (object sender, DialogClickEventArgs e)
                {
                    if (!string.IsNullOrEmpty(input.Text))
                    {
                        TaskyJ.Business.TaskyJManager.PushTask(new DBTaskJ { Name = input.Text, Description = input.Text }, CurrentSession);
                        RefreshList();
                    }
                });
                builder.SetNegativeButton(global::Android.Resource.String.Cancel, delegate (object sender, DialogClickEventArgs e) { });
                builder.Show();
            };

            Button cmdRefresh = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdRefresh);
            cmdRefresh.Click += delegate
            {
                RefreshList();
            };

            Button cmdEmptyTrash = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdEmptyTrash);
            cmdEmptyTrash.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Erase deleted tasks");
                builder.SetMessage("Are you sure?");
                builder.SetPositiveButton(global::Android.Resource.String.Ok, delegate (object sender, DialogClickEventArgs e)
                {
                    Business.TaskyJManager.ObliterateDeletedTasks(CurrentSession);
                    RefreshList();
                });
                builder.SetNegativeButton(global::Android.Resource.String.Cancel, delegate (object sender, DialogClickEventArgs e) { });
                builder.Show();
            };

            Switch swtShowDeleted = FindViewById<Switch>(TaskyJ.Interface.Android.Resource.Id.swtShowDeleted);
            swtShowDeleted.Click += delegate
            {
                RefreshList();
            };

            //initialize task list
            RefreshList();
        }

        private void RefreshList()
        {
            Button cmdEmptyTrash = FindViewById<Button>(TaskyJ.Interface.Android.Resource.Id.cmdEmptyTrash);
            cmdEmptyTrash.Enabled = false;
            var session = TaskyJManager.Authenticate("admin", "admin", "127.0.0.1");
            var lstTasks = TaskyJManager.RetrieveTasks(session).ToList();
            Switch swtShowDeletedTasks = FindViewById<Switch>(TaskyJ.Interface.Android.Resource.Id.swtShowDeleted);
            if (swtShowDeletedTasks != null)
            {
                if (swtShowDeletedTasks.Checked)
                {
                    var lstDeleted = TaskyJManager.RetrieveDeletedTasks(CurrentSession).ToList();
                    if (lstDeleted.Count > 0)
                    {
                        //split item
                        lstTasks.Add(new DBTaskJ
                        {
                            ID = -1,
                            Name = "---- Deleted ----"
                        });
                        lstTasks.AddRange(lstDeleted);
                        cmdEmptyTrash.Enabled = true;
                    }
                }
            }
            if (lstTasks.Count == 0)
            {
                lstTasks.Add(new DBTaskJ
                {
                    ID = -1,
                    Name = "<No tasks>"
                });
            }
            ListView lv = FindViewById<ListView>(TaskyJ.Interface.Android.Resource.Id.TaskListView);
            lv.Adapter = new ArrayAdapter<DBTaskJ>(this, global::Android.Resource.Layout.SimpleListItem1, lstTasks);
        }
    }
}
