   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   ,    &CarwashManager.Windows.NewClientWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	   -   5          � Title$  NewClientWindow��5   	   $	��600��6!   $	��900��6.    � 
ResizeMode$ NoResize=�6:    � WindowStartupLocation$ CenterScreen=�6P   .��5	      � -   5         �Name$ mainGrid��5	       � ColumnDefinitions 5
   
   �� 5      $	��200 �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��	-2,0,0,-3q�6r   ���	#FF8BD692 �� RadiusX$ 3��6�    �� RadiusY$ 3��6�   $��0��6�   �� 5   
   $��0��6   $��1��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��-35,0,-2,-5q�6r   ���	#FF4995BF$ 3��6�   $ 3��6�   $��0��6�   ]� 5   
   $��Stretch=�6   $��Stretch=�62   $
��True��6N   $��2��6c   .+�5      �� -   5        $��topBtnsPanel��5      $	��Top=�6-   $��Stretch=�6B   $	��Top=�6`   $��40��6x    �� Orientation$ 
Horizontal=�6�   $��0,0,0,10q�6�   .+�5      	� -   5         	�Name$ phBtn��5      $��Физ. лицо��6-   	 d� 	IsChecked$
	 TrueY�6A   
 	� 	GroupName$
 IsPhys��6R   $��	35,10,0,0q�6e   $��14(�6x   6�   	� 5      -   5        $ entBtn��5      $��Юр. лицо��6.   $
 IsPhys��6A   $��	35,10,0,0q�6T   $��14(�6g   5      �� 5      $��Bottom=�6   $��Bottom=�61   $��Right=�6L   $ 
Horizontal=�6h   $��10q�6�   .+�5      �� -   5         ��Name$ addBtn��5      $	��170��6)   $��32��65    �� 	IsDefault$
 True��6A   6R   �� 5      $	��170��6   $��32��6%    �� IsCancel$
 True��61   $��10,0,0,0q�6A      CancelButton#��[  6S   5      � 5      -   5        $ contentGrid��5      $��	10,0,10,0q�6(   5      5      5      