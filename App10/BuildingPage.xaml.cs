using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BuildingPage : Page
    {
        public string company_id;
        public string building_id;

        private const string url = "https://ah2015.azure-mobile.net/";
        private const string key = "vuEikZZQZTOknNzWBXClNARJbQIDUi82";
        private static MobileServiceClient MobileService = new MobileServiceClient(url, key);

        private IMobileServiceTable<Building> TABLE_BUILDING = MobileService.GetTable<Building>();
        private IMobileServiceTableQuery<Building> TABLE_BUILDING_QUERY;
        private List<Building> LIST_BUILDING;

        public BuildingPage()
        {
            this.InitializeComponent();
        }

        public async void update()
        {
            TABLE_BUILDING_QUERY = TABLE_BUILDING.Take(50).Where(x => x.company_id == company_id);
            LIST_BUILDING = await TABLE_BUILDING_QUERY.ToListAsync();

            try
            {
                no1.Text = LIST_BUILDING[0].total.ToString();
                building_id = LIST_BUILDING[0].id;
            }
            catch(Exception ex)
            {
                Frame.Navigate(typeof(HubPage));
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            company_id = e.Parameter.ToString();
            update();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HubPage));
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = building_id;

            Frame.Navigate(typeof(SectionPage), itemId);
        }
    }
}
