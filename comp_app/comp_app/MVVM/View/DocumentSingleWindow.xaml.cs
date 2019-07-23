﻿using comp_app.MVVM.Model;
using comp_app.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace comp_app.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для DocumentSingleWindow.xaml
    /// </summary>
    public partial class DocumentSingleWindow : Window
    {
        public DocumentSingleWindow(Document _document)
        {
            DataContext = new DocumentSingleViewModel(_document);
            InitializeComponent();
        }
    }
}
