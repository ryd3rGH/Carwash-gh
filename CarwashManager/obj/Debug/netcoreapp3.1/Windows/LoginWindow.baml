   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   (    "CarwashManager.Windows.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	   -   5          � Title$  LoginWindow��5   	   $	��206��6   $	��622��6*    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ CenterScreen=�6L   .��5	      � -   5         �Name$ mainGrid��5	       � ColumnDefinitions 5
   
   �� 5      $	��152 �6   �� 5      .+�5   
   �� $��0��6   $��0��6!   $��5��61   $	��Top=�6<   $��-2,0,0,0q�6T   ���	#FF8BD692 �� RadiusX$ 3��6w    �� RadiusY$ 3��6�   $��0��6�   �� 5   
   $��0��6   $��5��6!   $	��Top=�6,   $��
135,0,-2,0q�6D   ���	#FF4995BF$ 3��6i   $ 3��6u   $��0��6�   $��2��6�   �� 5   
   $��Логин:��6   $��1��6!    	  Label#��[  61   $	��Top=�6P   $
��Left=�6h   $��
144,39,0,0q�6�   �� 5   
   -   5         ��Name$ loginsComboBox��5      $
��Left=�6+   $	��Top=�6F   $	��236��6^    �� 
IsReadOnly$
 True��6j   $��14(�6|   $��1��6�   $��28��6�   $��
205,41,0,0q�6�   �� 5   
   $��Пароль:��6   $��1��6"   #��[  62   $	��Top=�6Q   $
��Left=�6i   $��
135,83,0,0q�6�   G� 5   
   -   5        	 G�Name$	 passTxt��5      $
��Left=�6'   $	��Top=�6B   $	��236��6Z   $��14(�6f   $��29��6t   $��1��6�   $��
205,83,0,0q�6�   �� 5   
   -   5        
 ��Name$
 enterBtn��5      $
��Left=�6#   $	��Top=�6>   $	��150��6V    �� 	IsDefault$
 True��6b   $��1��6�     EnterButton#��[ 6�   $��291,120,0,0q�6�   �� 5   
   $
��Left=�6   $	��112��6+   $	��Top=�68   $	��198��6P   $��2��6\   $��	40,40,0,0q�6p   $"��/Resources/Images/Logo_b.png��6�   5      5      