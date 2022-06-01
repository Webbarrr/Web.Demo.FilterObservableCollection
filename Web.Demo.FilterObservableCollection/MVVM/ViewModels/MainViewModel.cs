using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Web.Demo.FilterObservableCollection.MVVM.Models;

namespace Web.Demo.FilterObservableCollection.MVVM.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Cache of your data
        /// </summary>
        public ObservableCollection<Superhero> SuperHeroCache { get; set; }

        /// <summary>
        /// The filtered view to bind in your view
        /// </summary>
        public ICollectionView SuperHeroesView { get => CollectionViewSource.GetDefaultView(this.SuperHeroCache); }

        private string _searchCriteria;

        public string SearchCriteria
        {
            get => _searchCriteria;
            set
            {
                base.SetProperty(ref _searchCriteria, value); // use the base class to notify of the change to the property
                this.SuperHeroesView.Refresh(); // call the refresh method
            }
        }

        public MainViewModel()
        {
            // seed the cache with data
            var superHeroes = new List<Superhero>
            {
                new Superhero { FirstName = "Peter", LastName = "Parker", Alias = "Spider Man"},
                new Superhero { FirstName = "Tony", LastName = "Stark", Alias = "Iron Man"},
                new Superhero { FirstName = "Bruce", LastName = "Banner", Alias = "The Incredible Hulk"},
            };

            this.SuperHeroCache = new ObservableCollection<Superhero>(superHeroes);

            // bind the filter to a predicate
            this.SuperHeroesView.Filter = new Predicate<object>(p => this.Filter(p as Superhero));
        }

        private bool Filter(Superhero superHero)
        {
            return this.SearchCriteria == null
                || superHero.FirstName.IndexOf(this.SearchCriteria, StringComparison.OrdinalIgnoreCase) != -1
                || superHero.LastName.IndexOf(this.SearchCriteria, StringComparison.OrdinalIgnoreCase) != -1
                || superHero.Alias.IndexOf(this.SearchCriteria, StringComparison.OrdinalIgnoreCase) != -1;
        }
    }
}