   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   9    3CarwashManager.Windows.NewCarwashOrderLicenceWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	     � Title$"  NewCarwashOrderLicenceWindow��5   	   $	��250��6.   $	��350��6;    � WindowStartupLocation$ CenterOwner=�6G    � 
ResizeMode$ NoResize=�6k   .��5	      �  � RowDefinitions 5
   
   �� 5      $��10 �6   �� 5      �� 5      �� 5      �� 5      $��10 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��	-2,0,0,-3q�6r   ���	#FF8BD692 �� RadiusX$ 3��6�    �� RadiusY$ 3��6�   $��0��6�   �� 5   
   $��0��6   $��1��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��-35,0,-2,-5q�6r   ���	#FF4995BF$ 3��6�   $ 3��6�   $��0��6�   �� 5   
   $K��EВведите или выберите г/н авто клиента:��6    	  Label#��[  6A   $��1��6`   $��Bottom=�6m   $��Center=�6�   $��0,0,0,10q�6�   $��2��6�   �� 5   
   -   5        $��licencePlatesList��5      $��14(�6,   $��2��6:   $��Center=�6G   $	��Top=�6d   $	��200��6|   $��0,10,0,0q�6�    �� 
IsEditable$
 True��6�   $��2��6�   �� 5   
    �� Orientation$ 
Horizontal=�6   $��3��6.   $��2��6;   .+�5      �� -   5        $��goToOrderBtn��5        
NextButton#��[ 6)   $	��120��6M   	 �� 	IsDefault$
	 True��6Y   $��	50,0,10,0q�6j   6}   �� 5      
 �� IsCancel$

 True��6     CancelButton#��[ 6%   $	��120��6K   5      5      5      