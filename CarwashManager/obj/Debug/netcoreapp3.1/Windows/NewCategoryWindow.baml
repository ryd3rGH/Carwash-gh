   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   .    (CarwashManager.Windows.NewCategoryWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	   -   5          � Title$  NewCategoryWindow��5   	   $	��350��6#   $	��445��60    � 
ResizeMode$ NoResize=�6<   .��5	      � -   5         �Name$ mainGrid��5	       � ColumnDefinitions 5
   
   �� 5      $	��200 �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��	-2,0,0,-3q�6r   ���	#FF8BD692 �� RadiusX$ 3��6�    �� RadiusY$ 3��6�   $��0��6�   �� 5   
   $��0��6   $��1��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��-35,0,-2,-5q�6r   ���	#FF4995BF$ 3��6�   $ 3��6�   $��0��6�   ]� 5   
   $
��True��6   $��2��6)   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��30��62   $	��Top=�6>   $��0,10,0,0q�6S   .+�5      �� $ ��Имя категории:��6   $
��Left=�61   $��Center=�6L   $��14(�6g   $��10,0,0,0q�6u   6�   �� 5      -   5         ��Name$ nameTxt��5      $��14(�6+   $	��250��69   $��10,5,0,1q�6E   5      �� 5      $	��Top=�6   $
��Left=�6)   $	��Top=�6D   $��	10,10,0,5q�6\   $��14(�6o   $;��5Классы, входящие в категорию:��6}   �� 5      $ 
Horizontal=�6   $��Bottom=�62   $��30��6J   $��10q�6V   $��RightToLeft=�6b   .+�5      �� -   5         ��Name$ 	cancelBtn��5      $��Отмена��6,   $��14(�6=   	 �� IsCancel$
	 True��6K   $��80��6[   $��30��6f   �� 5      -   5        $ addBtn��5      
 �� 	IsDefault$

 True��6)   $��Добавить��6:   $��14(�6M   $��80��6[   $��30��6f   $��20,0,0,0q�6r   6�   5      � 5      -   5         �Name$ classesPanel��5       � Orientation$ Vertical=�6.   $��	10,0,10,0q�6E   5      5      5      