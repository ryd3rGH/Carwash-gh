   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CarwashManager.ControlsCarwashManager.Controls  5      2    ,CarwashManager.Controls.NewPhysClientControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:CarwashManager.Controls     5      -   5        .��5	      �   � ColumnDefinitions  5
   
   �� 5      �� 5      $
��3.5* �6    � RowDefinitions 5   
   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      .+�5   
   �� $��Bottom=�6   $��Right=�6+    	  Label#��[  6G   $��
Ф.И.О.:��6f   $��0��6x   $��0��6�   �� 5   
   $��Bottom=�6   $��Right=�6+   #��[  6G   $��Телефон:��6f   $��1��6y   $��0��6�   �� 5   
   $��Bottom=�6   $��Right=�6+   #��[  6G   $��E-mail:��6f   $��2��6x   $��0��6�   �� 5   
   $��Bottom=�6   $��Right=�6+   #��[  6G   $ ��Дата рождения:��6f   $��3��6   $��0��6�   �� 5   
   $��Bottom=�6   $��Right=�6+   #��[  6G   $$��Группа клиентов:��6f   $��4��6�   $��0��6�   �� 5   
   $��Bottom=�6   $��Right=�6+   #��[  6G   $��Автомобиль:��6f   $��5��6|   $��0��6�   �� 5   
   $	��Top=�6   $��0,20,0,0q�6(   $��Right=�6:   #��[  6V   $��Комментарий:��6u   $��6��6�   $��0��6�   �� 5   
   -   5         ��Name$ nameTxt��5      $��14(�6#   $��0��61   $��1��6>   $��Bottom=�6N   $	��300��6p   $
��Left=�6|   $��	15,20,0,3q�6�   �� 5   
   -   5        $ phoneTxt��5      $��14(�6$   $��1��62   $��1��6?   $��Bottom=�6O   $	��300��6q   $
��Left=�6}   $��	15,20,0,3q�6�    �� 	MaxLength$ 12��6�   6�   �� 5    
   -   5        $ mailTxt��5       $��14(�6#   $��2��61   $��1��6>   $��Bottom=�6N   $	��300��6p   $
��Left=�6|   $��	15,20,0,3q�6�   (  "System.Windows.Controls.DatePicker  5!   
   -   5          Name$ bDateTxt��5!      $��14(�6'   $��3��65   $��1��6B   $	��150��6R   $��Bottom=�6^   $
��Left=�6�   $��	15,15,0,3q�6�   �� 5"   
   -   5         ��Name$ 	groupList��5"      $��14(�6&   $��4��64   $��1��6A   $��Bottom=�6Q   $	��300��6s   $
��Left=�6   $��	15,20,0,3q�6�   �� 5#   
   -   5         ��Name$ 	addCarBtn��5#      $��+��6$   $��Center=�60   $��14(�6R   $��5��6`   $��1��6m   $��50��6}   $
��Left=�6�   $��	15,20,0,3q�6�   6�   �� 5$   
   -   5         ��Name$ carsLbl��5$      $��13(�6!   $��5��6/   $��1��6<   $��85,0,0,0q�6L   $��28��6^   $��Bottom=�6j   �� 5%   
   -	   5        $ 
commentTxt��5%      $��14(�6&   $��6��64   $��1��6A   $	��Top=�6Q   $	��300��6p   $
��Left=�6|   $��
15,20,0,50q�6�   5&      5'      