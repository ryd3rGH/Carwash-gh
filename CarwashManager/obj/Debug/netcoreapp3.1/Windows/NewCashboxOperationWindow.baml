   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:CarwashManager.WindowsCarwashManager.Windows  5   	   6    0CarwashManager.Windows.NewCashboxOperationWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CarwashManager.Windows     5   	     � Title$  NewCashboxOperationWindow��5   	   $	��290��6+   $	��445��68    � WindowStartupLocation$ CenterOwner=�6D    � 
ResizeMode$ NoResize=�6h   .��5	      � .+�5
   
   � $	��Top=�6   $��45��6'   ���	#FFBFBFBF.+�5      �� -   5         ��Name$ 
winNameLbl��5      $��Center=�6(   $��Center=�6C    	  Label#��[  6`   5      � 5   
   $��	0,40,0,75q�6    � RowDefinitions 5      �� 5      $��10 �6    �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��109* �6#   �� 5      $
��319* �6#   .+�5      �� $��Тип средств:��6   $��Right=�6+   $��Center=�6G   $��14(�6b   $��29��6p   $��1��6|   #��[  6�   �� 5      -   5         ��Name$ 	moneyList��5      $��14(�6*   $��1��68   $
��Left=�6H   $��20,0,0,0q�6c   $��Center=�6u   $	��225��6�   $��25��6�   $��1��6�   �� 5      $��Комментарий: ��6   $��Right=�6,   $��Center=�6H   $��14(�6c   $��29��6q   $��2��6}   #��[  6�   �� 5      -   5         ��Name$ 
commentTxt��5      $��1��6*   $
��Left=�6:   $��20,0,0,0q�6U   $��Center=�6g   $��14(�6�   $	��290��6�   $��2��6�   �� 5      $��Сумма:��6   $��Right=�6%   $��Center=�6A   $��14(�6\   $��3��6j   #��[  6w   �� 5      -   5        $ sumTxt��5      $��1��6'   $
��Left=�67   $��20,0,0,0q�6R   $��Center=�6d   $��14(�6   $��70��6�   $��3��6�    �� 	MaxLength$ 5��6�   6�   �� 5      $��0��6   $��0��6%   $��3��65   $��
-2,2,-35,0q�6@   $��Stretch=�6T   $	��Top=�6r   ���	#FF4995BF	 �� RadiusX$	 3��6�   
 �� RadiusY$
 3��6�   $��0��6�   �� 5      $��0��6   $��1��6%   $��2��65   $��7��6I   $��0,0,-2,5q�6T   $��Stretch=�6f   $	��Top=�6�   ���	#FF8BD692$	 3��6�   $
 3��6�   $��0��6�   5       �� 5!   
   -   5         ��Name$ 	cancelBtn��5!        CancelButton#��[ 6$   $��Right=�6J   $��	0,0,20,15q�6f   $��Bottom=�6y    �� IsCancel$
 True��6�   $	��160��6�   �� 5"   
   -   5        $ addBtn��5"      $��Right=�6!   $��
0,0,200,15q�6=   $��Bottom=�6Q    �� 	IsDefault$
 True��6l   6}   5#      5$      