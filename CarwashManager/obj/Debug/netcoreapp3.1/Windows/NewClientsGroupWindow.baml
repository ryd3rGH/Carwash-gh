   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   2    ,CarwashManager.Windows.NewClientsGroupWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	     � Title$  NewClientsGroupWindow��5   	   $	��250��6'   $	��445��64    � 
ResizeMode$ NoResize=�6@    � WindowStartupLocation$ CenterOwner=�6V   .��5	      � -   5         �Name$ mainGrid��5	       � ColumnDefinitions 5
   
   �� 5      $	��129 �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��	-2,0,0,-3q�6r   ���	#FF8BD692 �� RadiusX$ 3��6�    �� RadiusY$ 3��6�   $��0��6�   �� 5   
   $��0��6   $��1��6!   $��5��61   $��Stretch=�6<   $	��Top=�6Z   $��-35,0,-2,-5q�6r   ���	#FF4995BF$ 3��6�   $ 3��6�   $��0��6�   �� 5   
   $C��=Добавление новой группы клиентов��6    	  Label#��[  6;   $��Center=�6Z   $	��Top=�6w   $��0,10,0,0q�6�   $��2��6�   � 5   
   $��	0,45,0,50q�6   $��2��6"    5      �� 5      $
��129* �6#   �� 5      $
��319* �6#   .+�5      �� $��Название: ��6   $��Right=�6)   $��0,25,0,0q�6E   $	��Top=�6W   #��[  6o   �� 5      $!��Размер скидки: ��6   $��Right=�6.   $��0,70,0,0q�6J   $	��Top=�6\   #��[  6t   �� 5      -   5         ��Name$ groupNameTxt��5      $��14(�6,   $��1��6:   $
��Left=�6J   $��	20,27,0,0q�6e    �� TextWrapping$
 Wrap=�6x   $	��Top=�6�   $	��225��6�   $��25��6�   �� 5      -   5        $��
discSlider��5      $��1��6'   $	��Top=�67   $��Stretch=�6O   $��20, 75,20,0q�6m   	 � Minimum$	 0Q�6�   
 � Maximum$	
 100Q�6�    � Value$ 0Q�6�    �� TickFrequency$ 1Q�6�    �� AutoToolTipPlacement$ TopLeft=�6�    �� AutoToolTipPrecision$ 0��6�    �� IsSnapToTickEnabled$
 True��6�   5      �� 5   
   $��1��6   $��Right=�6%   $��Bottom=�6A    �� Orientation$ 
Horizontal=�6\   $��	0,0,10,10q�6u   .+�5      �� -   5         ��Name$ addBtn��5        AcceptButton#��[ 6%   $��0,0,10,0q�6K   $��Right=�6]   $��Bottom=�6y    �� 	IsDefault$
 True��6�   $	��100��6�   6�   �� 5      -   5        $ 	cancelBtn��5        CancelButton#��[ 6(   $��Right=�6N   $��Bottom=�6j    �� IsCancel$
 True��6�   $	��100��6�   5      5      5       