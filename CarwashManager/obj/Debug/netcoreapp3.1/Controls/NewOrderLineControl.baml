   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CarwashManager.ControlsCarwashManager.Controls  5      1    +CarwashManager.Controls.NewOrderLineControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:CarwashManager.Controls     5      $��70��5   "   $��Stretch=�6B   $��0,0,0,2q�6`   .��5	      �   � RowDefinitions  5
   
   �� 5      $	��33* �6   �� 5      $	��32* �6    � ColumnDefinitions 5   
   �� 5      $	��40* �6   �� 5      $	��90* �6   �� 5      $
��215* �6   �� 5      $
��103* �6   �� 5      $��33 �6   �� 5      $��33 �6   .+�5   
   �� ���	#FFBFBFBF �� RadiusX$ 10��6%    �� RadiusY$ 10��62   $��0,0,-10,-10q�6?   �� 5   
   ���	#FFBFBFBF$��1��6%   �� 5   
   ���	#FFBFBFBF$��0��6%   $��4��62   $��2��6B   �� 5   
   ���	#FFBFBFBF$ 10��6%   $ 10��62   $��-10,-10,0,0q�6?   $��1��6T   $��4��6a   $��2��6q   �� 5   
   ���	#FFBFBFBF$��0��6%   $��1��62   $��3��6B   $��2��6V   $��-2,0,0,0q�6g   �� 5   
   $��1��6   $��5��6!   $	��Top=�6,   $��32,32,146,0q�6D   ���	#FF8BD692$ 3��6j   $ 3��6v   $��0��6�   $��3��6�   �� 5   
   $��1��6   $��2��6!   $��5��61   $	��Top=�6<   $��
40,32,26,0q�6T   ���	#FF4995BF$ 3��6y   $ 3��6�   $��0��6�   $��2��6�   �� 5   
   -   5         ��Name$ numLbl��5      $��Center=�6    $��Center=�6;   $��14(�6X   $��2��6f   �� 5   
   -   5        $ plateLbl��5      $��Center=�6"   $��Center=�6=   $��1��6Z   $��14(�6j   $��2��6x   �� 5   
   -   5        $ servicesLbl��5      $��Center=�6%   $��Center=�6B   $��2��6]   $��14(�6m   $��2��6{   �� 5    
   -   5        $ 
estTimeLbl��5       $��Center=�6$   $��Center=�6?   $��3��6\   $��14(�6l   $��2��6z   �� 5!   
   -   5         ��Name$ endOrderBtn��5!      $��4��6&   $��0��66   $��1,1,1,1q�6C      CompleteSmallButton#��[  6T   6�   �� 5"   
   -   5        $ updateOrderBtn��5"      $��5��6)   $��0��69   $��1,1,1,1q�6F     EditSmallButton#��[ 6W   6�   �� 5#   
   -   5        $ delOrderBtn��5#      $��4��6&   $��2��66   $��1��6J   $��	18,0,18,0q�6W     DeleteSmallButton#��[ 6j   6�   5$      5%      