   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   ,    &CarwashManager.Windows.NewBoxModWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	   -   5          � Title$  NewBoxModWindow��5   	   $	��350��6!   $	��520��6.    � 
ResizeMode$ NoResize=�6:   .��5	      � .+�5
   
   � $	��Top=�6   $��45��6'   ���	#FFBFBFBF.+�5      �� -   5         ��Name$ 
winNameLbl��5      $��Center=�6(   $��Center=�6C    	  Label#��[  6`   5      � 5   
   $��	0,45,0,50q�6    � ColumnDefinitions 5      �� 5      �� 5      �� 5      .+�5      �� $��0��6   $��0��6%   $��2��65   $��3��6I   $��Stretch=�6T   $	��Top=�6r   $��-2,-2,-15, 0q�6�   ���	#FF4995BF �� RadiusX$ 3��6�    �� RadiusY$ 3��6�   $��0��6�   �� 5      $��0��6   $��2��6%   $��7��65   $��Stretch=�6@   $	��Top=�6^   $��-15,-4,-2,-5q�6v   ���	#FF8BD692$ 3��6�   $ 3��6�   $��0��6�   �� 5      $��Название: ��6   $��Right=�6)   $��0,42,0,0q�6E   $	��Top=�6W   #��[  6o   �� 5      $��Занятость: ��6   $��Right=�6*   $��0,87,0,0q�6F   $	��Top=�6X   #��[  6p   �� 5      -   5         ��Name$ 
boxNameTxt��5      $��14(�6*   $��1��68   $
��Left=�6H   $��	20,44,0,0q�6c    �� TextWrapping$
 Wrap=�6v   $	��Top=�6�   $	��225��6�   $��25��6�   $��2��6�   	� 5      -   5         	�Name$ 
notbusyBtn��5      $��Свободен��6.   $��1��6A   $
��Left=�6Q   $��	20,94,0,0q�6l   $	��Top=�6   $��14(�6�   	 	� 	GroupName$	 state��6�   
 d� 	IsChecked$

 TrueY�6�   	� 5      -   5        $ busyBtn��5      $��В работе��6+   $��1��6>   $
��Left=�6N   $��
149,94,0,0q�6i   $	��Top=�6}   $��14(�6�   $	 state��6�   $��2��6�   �� 5      $"��Тех. состояние: ��6   $��Right=�6/   $��	0,135,0,0q�6K   $	��Top=�6^   $��14(�6v   $��29��6�   $	��110��6�   #��[  6�   	� 5      -   5        $ IsWorkingBtn��5      $��Работает��60   $��1��6C   $
��Left=�6S   $��
20,142,0,0q�6n   $	��Top=�6�   $��14(�6�   $	 	techState��6�   $

 TrueY�6�   	� 5      -   5        $ IsNotWorkingBtn��5      $��В ремонте��63   $��1��6G   $
��Left=�6W   $��149,142,0,0q�6r   $	��Top=�6�   $��14(�6�   $	 	techState��6�   $��2��6�   5      �� 5   
   -   5         ��Name$ 	btnsPanel��5       �� Orientation$ 
Horizontal=�6(   $��Bottom=�6A   $��Right=�6\   $��50��6x   $��0,0,10,5q�6�   .+�5      �� -	   5         ��Name$ delBtn��5        DeleteButton#��[ 6%   $��5q�6K   $y�Hidden=�6V   6j   �� 5       -
   5        $ addBtn��5         AcceptButton#��[ 6%   $��5q�6K    �� 	IsDefault$
 True��6V   6g   �� 5!        CancelButton#��[ 6   $��5q�6;    �� IsCancel$
 True��6F   5"      5#      5$      