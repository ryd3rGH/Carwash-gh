   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   5    /CarwashManager.Windows.NewServiceCategoryWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	     � Title$  NewServiceCategoryWindow��5   	   $	��250��6*   $	��445��67    � WindowStartupLocation$ CenterOwner=�6C    � 
ResizeMode$ NoResize=�6g   .��5	      � -   5         �Name$ mainGrid��5	      .+�5
   
   �� $C��=Добавление новой категории услуг��6    	  Label#��[  6;   $��Center=�6Z   $	��Top=�6w   $��0,10,0,0q�6�   � 5   
   $��	0,45,0,50q�6    � ColumnDefinitions 5      �� 5      $
��129* �6#   �� 5      $
��319* �6#   .+�5      �� $��Название: ��6   $��Right=�6)   $��0,25,0,0q�6E   $	��Top=�6W   #��[  6o   �� 5      $"��Тех. состояние: ��6   $��Right=�6/   $��0,70,0,0q�6K   $	��Top=�6]   $��14(�6u   $��29��6�   $	��110��6�   $y�Hidden=�6�   �� 5      -   5         ��Name$ nameTxt��5      $��14(�6'   $��1��65   $
��Left=�6E   $��	20,27,0,0q�6`    �� TextWrapping$
 Wrap=�6s   $	��Top=�6�   $	��225��6�   $��25��6�   	� 5      -   5         	�Name$ IsWorkingBtn��5      $��Работает��60   $��1��6C   $
��Left=�6S   $��	20,77,0,0q�6n   $	��Top=�6�   $��14(�6�    	� 	GroupName$ 	techState��6�   	 d� 	IsChecked$
	 TrueY�6�   $y�Hidden=�6�   	� 5      -   5        $ IsNotWorkingBtn��5      $��В ремонте��63   $��1��6G   $
��Left=�6W   $��
149,77,0,0q�6r   $	��Top=�6�   $��14(�6�   $ 	techState��6�   $y�Hidden=�6�   5      �� 5   
   -   5        
 ��Name$
 	cancelBtn��5        CancelButton#��[ 6$   $��Right=�6J   $��	0,0,20,15q�6f   $��Bottom=�6y    �� IsCancel$
 True��6�   $	��150��6�   �� 5   
   -   5        $
 addBtn��5        AddServiceTypeButton#��[ 6!   $
��Left=�6O   $��
115,0,0,15q�6j   $��Bottom=�6~    �� 	IsDefault$
 True��6�   $	��150��6�   6�   5      5      