using FootballersMVVM.Commands;
using FootballersMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballersMVVM.ViewModels
{
    public class FootballersViewModel : ViewModelBase
    {
        private readonly string serializationPath = "saved.txt";

        private ObservableCollection<FootballerModel> _footballerList = new ObservableCollection<FootballerModel>();
        private int _selectedFootIndex = -1;

        private string _textBoxFirstName = "";
        private string _textBoxLastName = "";
        private int _sliderWeight = 0;
        private int _sliderAge = 0;

        private ICommand _buttonAdd = null;
        private ICommand _buttonRemove = null;
        private ICommand _buttonEdit = null;
        private ICommand _loadFootballerList = null;
        private ICommand _saveFootballerList = null;

        public FootballersViewModel()
        {
            if (File.Exists(serializationPath))
            {
                FootballerList = new ObservableCollection<FootballerModel>(FootballerSerialization.LoadFrom(serializationPath));
            }
        }

        private bool CanBeAdded(string text)
        {
            if (String.IsNullOrEmpty(text))
                return false;
            if (Regex.IsMatch(text, @"\d"))
                return false;
            return true;
        }

        private void AddFootballer()
        {
            var footballer = new FootballerModel(TextBoxFirstName, TextBoxLastName, SliderWeight, SliderAge);
            _footballerList.Add(footballer);
            FootballerSerialization.SaveTo(serializationPath, FootballerList.ToList());
        }

        private void RemoveFootballer()
        {
            var atIndex = SelectedFootIndex;
            SelectedFootIndex = -1;
            FootballerList.RemoveAt(atIndex);
            FootballerSerialization.SaveTo(serializationPath, FootballerList.ToList());
        }

        private void EditFootballer()
        {
            var footballer = new FootballerModel(TextBoxFirstName, TextBoxLastName, SliderWeight, SliderAge);
            FootballerList.Insert(SelectedFootIndex, footballer);
            SelectedFootIndex -= 1;
            FootballerList.RemoveAt(SelectedFootIndex + 1);
            FootballerSerialization.SaveTo(serializationPath, FootballerList.ToList());
        }

        #region Get/Set Commands
        public ICommand LoadFootballerList
        {
            get 
            { 
                if(_loadFootballerList == null)
                {
                    _loadFootballerList = new RelayCommand( 
                        arg => FootballerList = new ObservableCollection<FootballerModel>(FootballerSerialization.LoadFrom(serializationPath)),
                        arg => File.Exists(serializationPath) );
                }
                return _loadFootballerList; 
            }
        }

        public ICommand SaveFootballerList
        {
            get
            {
                if (_saveFootballerList == null)
                {
                    _saveFootballerList = new RelayCommand(
                        arg => FootballerSerialization.SaveTo(serializationPath, FootballerList.ToList()),
                        arg => true );
                }
                return _saveFootballerList;
            }
        }

        public ICommand ButtonEdit
        {
            get
            {
                if (_buttonEdit == null)
                {
                    _buttonEdit = new RelayCommand(
                        arg => EditFootballer(),
                        arg => CanBeAdded(TextBoxFirstName) && CanBeAdded(TextBoxLastName) && SelectedFootIndex > -1);
                }
                return _buttonEdit;
            }
        }

        public ICommand ButtonRemove
        {
            get
            {
                if (_buttonRemove == null)
                {
                    _buttonRemove = new RelayCommand(
                        arg => RemoveFootballer(),
                        arg => SelectedFootIndex > -1);
                }
                return _buttonRemove;
            }
        }

        public ICommand ButtonAdd
        {
            get 
            { 
                if(_buttonAdd == null)
                {
                    _buttonAdd = new RelayCommand( 
                        arg => AddFootballer(), 
                        arg => CanBeAdded(TextBoxFirstName) && CanBeAdded(TextBoxLastName));
                }
                return _buttonAdd;
            }
        }
        #endregion

        #region Get/Set
        public ObservableCollection<FootballerModel> FootballerList 
        {
            get
            {
                return _footballerList;
            }
            set
            {
                _footballerList = value;
                OnPropertyChanged(nameof(FootballerList));
            }
        }

        public int SelectedFootIndex
        {
            get { return _selectedFootIndex; }
            set 
            { 
                _selectedFootIndex = value;

                if (_selectedFootIndex > -1)
                {
                    TextBoxFirstName = FootballerList[_selectedFootIndex].FirstName;
                    TextBoxLastName = FootballerList[_selectedFootIndex].LastName;
                    SliderAge = FootballerList[_selectedFootIndex].Age;
                    SliderWeight = FootballerList[_selectedFootIndex].Weight;
                }
                else
                {
                    TextBoxFirstName = "";
                    TextBoxLastName = "";
                    SliderAge = 0;
                    SliderWeight = 0;
                }

                OnPropertyChanged(nameof(SelectedFootIndex)); }
        }


        public string TextBoxFirstName
        {
            get { return _textBoxFirstName; }
            set { _textBoxFirstName = value; OnPropertyChanged(nameof(TextBoxFirstName)); }
        }

        public string TextBoxLastName
        {
            get { return _textBoxLastName; }
            set { _textBoxLastName = value; OnPropertyChanged(nameof(TextBoxLastName)); }
        }

        public int SliderAge
        {
            get { return _sliderAge; }
            set { _sliderAge = value; OnPropertyChanged(nameof(SliderAge), nameof(LabelAge)); }
        }

        public int SliderWeight
        {
            get { return _sliderWeight; }
            set { _sliderWeight = value; OnPropertyChanged(nameof(SliderWeight), nameof(LabelWeight)); }
        }

        public string LabelAge
        {
            get { return $"Wiek ({_sliderAge})"; }
        }

        public string LabelWeight
        {
            get { return $"Waga ({_sliderWeight})"; }
        }
        #endregion
    }
}
