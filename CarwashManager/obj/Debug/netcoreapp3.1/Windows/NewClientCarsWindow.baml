   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   0    *CarwashManager.Windows.NewClientCarsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	   -   5          � Title$  NewClientCarsWindow��5   	   $	��450��6%   $	��900��62    � WindowStartupLocation$ CenterScreen=�6>    � 
ResizeMode$ NoResize=�6c   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��200 �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��	-2,0,0,-3q�6r   ���	#FF8BD692 �� RadiusX$ 3��6�    �� RadiusY$ 3��6�   $��0��6�   �� 5   
   $��0��6   $��1��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��-35,0,-2,-5q�6r   ���	#FF4995BF$ 3��6�   $ 3��6�   $��0��6�   ]� 5   
   $
��True��6   $��2��6)   .+�5      �� -   5         ��Name$ topPanel��5      $��Stretch=�6+   $	��Top=�6I   $	��Top=�6a    �� Orientation$ 
Horizontal=�6w   $��	10,3,10,0q�6�   .+�5      ��  	  Label#��[  6   $��Марка:��67   $��Bottom=�6H   �� 5      -   5         ��Name$ 
brandsList��5      $��14(�6/   $	��150��6=   $��	10,14,0,2q�6I   	 �� 
IsEditable$
	 True��6\   6n   �� 5      #��[  6   $��Модель:��67   $��Bottom=�6I   $��10,0,0,0q�6k   �� 5      -   5        $ 
modelsList��5      $��14(�6/   $	��200��6=   $��	10,14,0,2q�6I   $
	 True��6\   �� 5      #��[  6   $��Гос.номер:��67   $��Bottom=�6L   $��10,0,0,0q�6n   6�   �� 5      -   5        
 ��Name$
 plateTxt��5      $��90��6,   $��	10,14,0,2q�67   �� 5      -   5         ��Name$ 	addCarBtn��5      $��Right=�6,     AddClientsCarButton#��[ 6H    �� 	IsDefault$
 True��6u   $��20,7,0,0q�6�   5      � 5      $	��Top=�6   $��32��6(   $	��Top=�64   $��Stretch=�6L   $��	10,10,0,0q�6j   .+�5      �� #��[  6   $*��$Автомобили клиента:��67   5      �� 5      $��Right=�6   $��Bottom=�65   $��Bottom=�6P   $��34��6h   $��
10,0,10,10q�6t   .+�5      �� $
��Left=�6   $	��120��64     CancelButton#��[ 6@    �� IsCancel$
 True��6f   5      � 5       $��Stretch=�6   $��Stretch=�61   .+�5!      �� $
��Auto=�6   $��
10,0,10,10q�6B   
�������.��5"      �� -   5        $ 	carsPanel��5"   !   $ Vertical=�64   6M   5#      5$      5%      5&      5'      