using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class ViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private string _health = "";
    private string _speed = "";

    public event PropertyChangedEventHandler PropertyChanged;
    [Binding]
    public string Health 
    {
        get => _health;
        set 
        {
            if (_health.Equals(value)) return;
            _health = value;
            OnPropertyChanged("Health");
        }  
    }
    [Binding]
    public string Speed
    {
        get => _speed;
        set
        {
            if (_speed.Equals(value)) return;
            _speed = value;
            OnPropertyChanged("Speed");
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        if(PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
