using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Heritage.Wpf.Controls;

/// <summary>
/// TimePicker.xaml の相互作用ロジック
/// </summary>
public partial class TimePicker : UserControl, INotifyPropertyChanged
{
	private bool _isUpdating;
	public ObservableCollection<string> Hours { get; } = new();
	public ObservableCollection<string> Minutes { get; } = new();
	public ObservableCollection<string> Periods { get; } = new();

	private string _selectedHour;
	public string SelectedHour
	{
		get => _selectedHour;
		set
		{
			if (_selectedHour == value)
				return;

			_selectedHour = value;
			OnPropertyChanged();

			UpdateSelectedTimeFromSelections();
		}
	}

	private string _selectedMinute;
	public string SelectedMinute
	{
		get => _selectedMinute;
		set
		{
			if (_selectedMinute == value)
				return;

			_selectedMinute = value;
			OnPropertyChanged();

			UpdateSelectedTimeFromSelections();
		}
	}

	private string _selectedPeriod;
	public string SelectedPeriod
	{
		get => _selectedPeriod;
		set
		{
			if (_selectedPeriod == value)
				return;

			_selectedPeriod = value;
			OnPropertyChanged();

			UpdateSelectedTimeFromSelections();
		}
	}

	public Visibility PeriodVisibility =>
		ClockIdentifier == TimePickerClockIdentifier.Clock12Hour
			? Visibility.Visible
			: Visibility.Collapsed;

	public event PropertyChangedEventHandler? PropertyChanged;

	#region Dependency Properties

	public static readonly DependencyProperty SelectedTimeProperty =
		DependencyProperty.Register(
			nameof(SelectedTime),
			typeof(TimeSpan?),
			typeof(TimePicker),
			new FrameworkPropertyMetadata(
				null,
				FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
				OnSelectedTimeChanged));

	public TimeSpan? SelectedTime
	{
		get => (TimeSpan?)GetValue(SelectedTimeProperty);
		set => SetValue(SelectedTimeProperty, value);
	}

	public static readonly DependencyProperty ClockIdentifierProperty =
		DependencyProperty.Register(
			nameof(ClockIdentifier),
			typeof(TimePickerClockIdentifier),
			typeof(TimePicker),
			new FrameworkPropertyMetadata(
				TimePickerClockIdentifier.Clock24Hour,
				OnClockIdentifierChanged));

	public TimePickerClockIdentifier ClockIdentifier
	{
		get => (TimePickerClockIdentifier)GetValue(ClockIdentifierProperty);
		set => SetValue(ClockIdentifierProperty, value);
	}

	public static readonly DependencyProperty MinuteIncrementProperty =
		DependencyProperty.Register(
			nameof(MinuteIncrement),
			typeof(int),
			typeof(TimePicker),
			new FrameworkPropertyMetadata(
				1,
				OnMinuteIncrementChanged,
				CoerceMinuteIncrement));

	public int MinuteIncrement
	{
		get => (int)GetValue(MinuteIncrementProperty);
		set => SetValue(MinuteIncrementProperty, value);
	}

	#endregion

	public TimePicker()
	{
		InitializeComponent();

		Periods.Add("AM");
		Periods.Add("PM");

		RebuildHours();
		RebuildMinutes();

		ApplySelectedTimeToSelections(SelectedTime);
	}

	private void RebuildHours()
	{
		Hours.Clear();

		if (ClockIdentifier == TimePickerClockIdentifier.Clock24Hour)
		{
			for (var i = 0; i < 24; i++)
			{
				Hours.Add(i.ToString("00"));
			}
		}
		else
		{
			for (var i = 1; i <= 12; i++)
			{
				Hours.Add(i.ToString("00"));
			}
		}
	}

	private void RebuildMinutes()
	{
		Minutes.Clear();

		for (var i = 0; i < 60; i += MinuteIncrement)
		{
			Minutes.Add(i.ToString("00"));
		}
	}

	private void UpdateSelectedTimeFromSelections()
	{
		if (_isUpdating)
			return;

		if (SelectedHour is null || SelectedMinute is null)
		{
			SelectedTime = null;
			return;
		}

		if (!int.TryParse(SelectedHour, out var hour))
		{
			SelectedTime = null;
			return;
		}

		if (!int.TryParse(SelectedMinute, out var minute))
		{
			SelectedTime = null;
			return;
		}

		if (ClockIdentifier == TimePickerClockIdentifier.Clock12Hour)
		{
			if (SelectedPeriod is null)
			{
				SelectedTime = null;
				return;
			}

			if (SelectedPeriod == "AM")
			{
				if (hour == 12)
				{
					hour = 0;
				}
			}
			else
			{
				if (hour != 12)
				{
					hour += 12;
				}
			}
		}

		SelectedTime = new TimeSpan(hour, minute, 0);
	}

	private void ApplySelectedTimeToSelections(TimeSpan? selectedTime)
	{
		try
		{
			_isUpdating = true;

			if (selectedTime is null)
			{
				SelectedHour = null;
				SelectedMinute = null;
				SelectedPeriod = null;
				return;
			}

			var time = selectedTime.Value;
			var hour = time.Hours;
			var minute = NormalizeMinute(time.Minutes);

			if (ClockIdentifier == TimePickerClockIdentifier.Clock24Hour)
			{
				SelectedHour = hour.ToString("00");
				SelectedPeriod = null;
			}
			else
			{
				SelectedPeriod = hour < 12 ? "AM" : "PM";

				var hour12 = hour % 12;
				if (hour12 == 0)
				{
					hour12 = 12;
				}

				SelectedHour = hour12.ToString("00");
			}

			SelectedMinute = minute.ToString("00");
		}
		finally
		{
			_isUpdating = false;
		}
	}

	private int NormalizeMinute(int minute)
	{
		if (MinuteIncrement <= 1)
		{
			return minute;
		}

		var normalized = minute / MinuteIncrement * MinuteIncrement;

		if (normalized >= 60)
		{
			normalized = 60 - MinuteIncrement;
		}

		return normalized;
	}

	private void ClearSelections()
	{
		try
		{
			_isUpdating = true;

			SelectedHour = null;
			SelectedMinute = null;
			SelectedPeriod = null;
		}
		finally
		{
			_isUpdating = false;
		}
	}


	private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	private static void OnSelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var control = (TimePicker)d;

		if (control._isUpdating)
			return;

		var value = (TimeSpan?)e.NewValue;

		control.ApplySelectedTimeToSelections(value);
	}

	private static void OnClockIdentifierChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var control = (TimePicker)d;

		control.OnPropertyChanged(nameof(PeriodVisibility));

		control.RebuildHours();
		control.ApplySelectedTimeToSelections(control.SelectedTime);
		control.UpdateSelectedTimeFromSelections();
	}

	private static void OnMinuteIncrementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var control = (TimePicker)d;

		control.RebuildMinutes();
		control.ApplySelectedTimeToSelections(control.SelectedTime);
		control.UpdateSelectedTimeFromSelections();
	}

	private static object CoerceMinuteIncrement(DependencyObject d, object baseValue)
	{
		var value = (int)baseValue;

		// 1 ～ 60 までの範囲しか有効ではない
		if (value < 1)
		{
			return 1;
		}

		if (value > 60)
		{
			return 60;
		}

		return value;
	}
}
