   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   )    #CarwashManager.Windows.NewBoxWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	   -   5          � Title$  NewBoxWindow��5   	   $	��250��6   $	��445��6+    � 
ResizeMode$ NoResize=�67   .��5	      � -   5         �Name$ mainGrid��5	      .+�5
   
   �� $2��,Добавление нового бокса��6   $��14(�62   $��Center=�6@   $	��Top=�6]   $��0,10,0,0q�6u   � 5   
   $��	0,45,0,50q�6    � ColumnDefinitions 5      �� 5      $
��129* �6#   �� 5      $
��319* �6#   .+�5      �� $��Название: ��6   $��Right=�6)   $��0,25,0,0q�6E   $	��Top=�6W   $��14(�6o   $��29��6}   $��78��6�   �� 5      $"��Тех. состояние: ��6   $��Right=�6/   $��0,70,0,0q�6K   $	��Top=�6]   $��14(�6u   $��29��6�   $	��110��6�   �� 5      -   5         ��Name$ 
boxNameTxt��5      $��14(�6*   $��1��68   $
��Left=�6H   $��	20,27,0,0q�6c    �� TextWrapping$
 Wrap=�6v   $	��Top=�6�   $	��225��6�   $��25��6�   	� 5      -   5         	�Name$ IsWorkingBtn��5      $��Работает��60   $��1��6C   $
��Left=�6S   $��	20,77,0,0q�6n   $	��Top=�6�   $��14(�6�    	� 	GroupName$ 	techState��6�    d� 	IsChecked$
 TrueY�6�   	� 5      -   5        $ IsNotWorkingBtn��5      $��В ремонте��63   $��1��6G   $
��Left=�6W   $��
149,77,0,0q�6r   $	��Top=�6�   $��14(�6�   $ 	techState��6�   5      �� 5   
   -   5        	 ��Name$	 	cancelBtn��5      $��Отмена��6$   $��14(�65   $��Right=�6C   $��	0,0,20,15q�6_   $��Bottom=�6r   
 �� IsCancel$

 True��6�   $��85��6�   $��30��6�   �� 5   
   -   5        $	 addBtn��5      $��Добавить��6!   $��14(�64   $��Right=�6B   $��
0,0,122,15q�6^   $��Bottom=�6r    �� 	IsDefault$
 True��6�   $��85��6�   $��30��6�   6�   5      5      