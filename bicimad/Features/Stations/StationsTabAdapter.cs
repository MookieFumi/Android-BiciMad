using System.Collections.Generic;
using System.Linq;
using Java.Lang;

namespace bicimad.Features.Stations
{
    public class StationsTabAdapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        private readonly List<StationsTab> _tabs;

        public StationsTabAdapter(Android.Support.V4.App.FragmentManager fragmentManager, List<StationsTab> tabs) : base(fragmentManager)
        {
            _tabs = tabs;
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
    }
}