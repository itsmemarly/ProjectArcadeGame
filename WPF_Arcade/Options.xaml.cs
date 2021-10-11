using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Arcade
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
            Focus();
        }

        /*
         * MasterVolumeSlider.Value = MasterVolume
         * MusicVolumeSlider.Value = MusicVolume
         * MasterVolumeSlider.Value = MasterVolume
         */

        private void MasterVolumeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.MasterVolume = 0;
        }

        private void MasterVolumeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // MasterVolume is MasterVolume
        }

        private void MusicVolumeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // MusicVolume is 0
        }

        private void MusicVolumeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // MusicVolume is MusicVolume
        }

        private void EffectVolumeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // EffectVolume is 0
        }

        private void EffectVolumeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // EffectVolume is EffectVolume
        }

        private void MasterVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // MasterVolume = MasterVolumeSlider.Value
        }

        private void MusicVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // MusicVolume = MusicVolumeSlider.Value
        }

        private void EffectVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // EffectVolume = EffectVolumeSlider.Value
        }

        private void TerugNaarMenu_Clicked(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            Close();
            menu.Visibility = Visibility.Visible;
        }
    }
}
