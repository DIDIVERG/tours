   M S B A M L   `   `   `  ����A  =Second, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Second.Views.SeasonDialogSecond.Views.SeasonDialog  5   	   ,    &Second.Views.SeasonDialog.SeasonDialog   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   9local'clr-namespace:Second.Views.SeasonDialog     5   	     � Title$  SeasonDialog��5   	   $	��450��6   $	��800��6+   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��0��6   $��
Season ID:��6-   �� 5   
   $��0��6   $��1��6    �� 
IsReadOnly$
 True��6/   ��6A   �� *
SeasonId+�� 5   
   $��1��6   $��0��6   $��Tour ID:��6-   �� 5   
   $��1��6   $��1��6   ��6/   �� *TourId+�� 5   
   $��2��6   $��0��6   $��Seat Amount:��6-   �� 5    
   $��2��6   $��1��6   ��6/   �� *
SeatAmount+�� 5"   
   $��3��6   $��0��6   $��Start Date:��6-   (  "System.Windows.Controls.DatePicker  5#   
   -   5          Name$ StartDatePicker��5#      $��3��6.   $��1��6;      SelectedDate 6K   �� *	StartDate+�� 5%   
   $��4��6   $��0��6   $��	End Date:��6-     5&   
   -   5        $ EndDatePicker��5&      $��4��6,   $��1��69    6I   �� *	EndDate+�� 5(   
   $��5��6   $��0��6   $��Closed:��6-   �� 5)   
   $��5��6   $��1��6     d� 	IsChecked 60   �� *Closed+�� 5+   
   $��6��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   $��0,10,0,0q�6g   .+�5,      �� -   5        $��5��5,      $��0��6"   $��OK��62   $	��100��6?   $��5q�6K   6V   �� 5-      -   5        $��5��5-      $��1��6"   $��Cancel��62   $	��100��6C   $��5q�6O   6Z   5.      5/      50      