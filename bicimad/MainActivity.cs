using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using bicimad.Features.Stations;
using bicimad.Features.Stations.Models.Entities;
using bicimad.Infrastructure.Transformers;
using Java.Lang;

namespace bicimad
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, IStationsView
    {
        private StationsPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            SetupPresenter();

            SetupTabs();
            SetupToolbar();
        }

        private void SetupPresenter()
        {
            _presenter = new StationsPresenter(this);
            _presenter.StationsLoaded += (sender, e) =>
            {
                Toast.MakeText(this, $"Total stations: {e}", ToastLength.Short).Show();
                //_stationAdapter.NotifyDataSetChanged();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.StationsMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                //case Android.Resource.Id.Home:
                //    drawerLayout.OpenDrawer(GravityCompat.Start);
                //    return true;
                //case Resource.Id.Update:
                //    RunOnUiThread(async () =>
                //    {
                //        await _presenter.GetStationsAsync();
                //    });
                //    break;
                //case Resource.Id.About:
                //    Toast.MakeText(this, "About clicked", ToastLength.Short).Show();
                //    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void SetupTabs()
        {
            var mainPageAdapter = new MainPageAdapter(SupportFragmentManager, _presenter);
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            viewPager.Adapter = mainPageAdapter;
            viewPager.SetPageTransformer(true, new ScaleTransformer());

            //Attach item selected handler to navigation view
            //var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            //navigationView.NavigationItemSelected += (sender, args) =>
            //{
            //    Toast.MakeText(this, args.MenuItem.ItemId, ToastLength.Short).Show();

            //};
            //navigationView.InflateMenu(Resource.Menu.StationsMenu);

            //drawerLayout = (DrawerLayout)FindViewById<DrawerLayout>(Resource.Id.drawerLayout);


            //_navigationView.InflateHeaderView(Resource.Layout.MainToolbar);

            //// Create ActionBarDrawerToggle button and add it to the toolbar
            //var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            //drawerLayout.SetDrawerListener(drawerToggle);
            //drawerToggle.SyncState();

            ////Load default screen
            //var ft = FragmentManager.BeginTransaction();
            //ft.AddToBackStack(null);
            //ft.Add(Resource.Id.HomeFrameLayout, new HomeFragment());
            //ft.Commit();
        }

        private void SetupToolbar()
        {
            //var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Toolbar);
            //SetSupportActionBar(toolbar);
            //SupportActionBar.Title = GetString(Resource.String.app_name);

            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_action_menu);
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
        }

        public void OnStationClick(Station station)
        {
            //var fragmentManager = SupportFragmentManager.BeginTransaction();
            //var dialog = StationDialogFragment.NewInstance(station);
            //dialog.Show(fragmentManager, nameof(StationDialogFragment));
        }

        public void Busy(bool busy)
        {
            //_toolbar.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
        }
    }

    public class MainPageAdapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        private readonly List<MainPageTab> _tabs;
        public MainPageAdapter(Android.Support.V4.App.FragmentManager fragmentManager, StationsPresenter presenter) : base(fragmentManager)
        {
            //var players = new PlayersServices().GetPlayers().ToList();
            _tabs = new List<MainPageTab>
            {
                new MainPageTab(StationsFragment.NewInstance(presenter), "Todas") ,
                //new MainPageTab(PlayerListFragment.NewInstance(players), "All players") ,
                //new MainPageTab(PlayerListFragment.NewInstance(players.Where(p =>p.Country.Equals("Spain", StringComparison.InvariantCultureIgnoreCase))), "Spanish players"),
                //new MainPageTab(PlayerListFragment.NewInstance(players.Where(p =>p.Country.Equals("United States", StringComparison.InvariantCultureIgnoreCase))),"USA players")
            };
        }

        public override int Count => _tabs.Count;

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_tabs.ElementAt(position).Title.ToUpper());
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _tabs.ElementAt(position).Fragment;
        }

        public class MainPageTab
        {
            public MainPageTab(Android.Support.V4.App.Fragment fragment, string title)
            {
                Fragment = fragment;
                Title = title;
            }

            public Android.Support.V4.App.Fragment Fragment { get; }
            public string Title { get; }
        }
    }
}