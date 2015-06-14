using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class SectionPage : Page
    {
        public string building_id;

        private const string url = "https://ah2015.azure-mobile.net/";
        private const string key = "vuEikZZQZTOknNzWBXClNARJbQIDUi82";
        private static MobileServiceClient MobileService = new MobileServiceClient(url, key);

        private IMobileServiceTable<Section> TABLE_SECTION = MobileService.GetTable<Section>();
        private IMobileServiceTableQuery<Section> TABLE_SECTION_QUERY;
        private List<Section> LIST_SECTION;

        public SectionPage()
        {
            this.InitializeComponent();
        }

        public async void update()
        {
            TABLE_SECTION_QUERY = TABLE_SECTION.Take(50).Where(x => x.building_id == building_id);
            LIST_SECTION = await TABLE_SECTION_QUERY.ToListAsync();

            no1.Text = LIST_SECTION[0].total.ToString();
            no2.Text = LIST_SECTION[1].total.ToString();
            no3.Text = LIST_SECTION[2].total.ToString();
            no4.Text = LIST_SECTION[3].total.ToString();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            building_id = e.Parameter.ToString();
            update();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var itemId = building_id;

            Frame.Navigate(typeof(BuildingPage), itemId);
        }
    }
}
