   M S B A M L   `   `   `  ����A  =Second, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Second.Views.PaymentDialogSecond.Views.PaymentDialog  5   	   5clr-namespace:Second.ViewModelsSecond.ViewModels  5   	   .    (Second.Views.PaymentDialog.PaymentDialog   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :local(clr-namespace:Second.Views.PaymentDialog     5   	   6
viewModelsclr-namespace:Second.ViewModels     5   	     � Title$  PaymentDialog��5   	   $	��200��6   $	��800��6,   .��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��0��6   $��Payment Id:��6-   �� 5   
   $��0��6   $��1��6    �� 
IsReadOnly$
 True��6/   ��6A   �� *	PaymentId+�� 5   
   $��1��6   $��0��6   $��Voucher Id:��6-   �� 5   
   $��1��6   $��1��6   ��6/   �� *	VoucherId+�� 5   
   $��2��6   $��0��6   $��Payment Date:��6-   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
DatePicker��5      $��2��6)   $��1��66      SelectedDate 6F   �� *PaymentDate+�� 5   
   $��3��6   $��0��6   $��Price:��6-   �� 5    
   $��3��6   $��1��6   ��6/   �� *Price+�� 5"   
   -   5        $��5��5"      $��0��6   $��OK��6.   $	��100��6;   $��5q�6G   6R   �� 5#   
   -   5        $��5��5#      $��1��6   $��Cancel��6.   $	��100��6?   $��5q�6K   6V   5$      5%      