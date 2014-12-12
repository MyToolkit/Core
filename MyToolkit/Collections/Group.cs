﻿//-----------------------------------------------------------------------
// <copyright file="Group.cs" company="MyToolkit">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>http://mytoolkit.codeplex.com/license</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyToolkit.Collections
{
    /// <summary>The interface of a group. </summary>
    public interface IGroup
    {
        string Title { get; }
    }

    /// <summary>An group implementation with a title and a list of items. </summary>
    /// <typeparam name="T">The item type. </typeparam>
    public class Group<T> : ObservableCollection<T>, IGroup
    {
        private string _title;

        public Group(string title) : this(title, new List<T>()) { }

        public Group(string title, IEnumerable<T> items)
            : base(items)
        {
            Title = title;
        }

        /// <summary>Gets or sets the title of the group. </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Title"));
                }
            }
        }

        /// <summary>Raises the System.Collections.ObjectModel.ObservableCollection{T}.PropertyChanged event with the provided arguments. </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == "Count")
                OnPropertyChanged(new PropertyChangedEventArgs("HasItems"));
        }

        /// <summary>Gets a value indicating whether the group has items. </summary>
        public bool HasItems
        {
            get { return Items.Count > 0; }
        }
    }
}
