   M S B A M L   `   `   `  ����I  ECarwashManager, Version=0.0.0.1, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CarwashManager.ControlsCarwashManager.Controls  5      @ <CWLib, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null,"clr-namespace:CWLib;assembly=CWLibCWLib 5      3    -CarwashManager.Controls.ClientGroupBigControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:CarwashManager.Controls     5      2lib"clr-namespace:CWLib;assembly=CWLib     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��35 �6   �� 5      $��32 �6   �� 5      $��32 �6   �� 5      $��32 �6   �� 5      $��32 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $��0.65* �6   �� 5      �� 5      $
��0.6* �6   .+�5   
   �� $$��Название группы:��6   $��1��6+   $��0��68   $��Center=�6H   $��Right=�6c   $��14(�6   �� 5   
   $'��!Скидка для группы:��6   $��2��6-   $��0��6:   $��Center=�6J   $��Right=�6e   $��14(�6�   �� 5   
   -   5         ��Name$ groupNameLbl��5      $��1��6&   $��1��63   $��14(�6C   $
��Left=�6Q   $��Stretch=�6s   $��10,0,0,0q�6�   �� 5   
   -   5        $ groupDiscountlbl��5      $��2��6*   $��1��67   $��14(�6G   $
��Left=�6U   $��Stretch=�6w   $��10,0,0,0q�6�   �� 5   
   -   5         ��Name$ delClientGroupBtn��5      $��Удалить��6,   $��4��6>   $��2��6K   $��	15,3,25,3q�6[   $��14(�6n   �� 5   
   -   5        $ updateClientGroupBtn��5      $��Изменить��6/   $��4��6B   $��1��6O   $��
134,3,15,3q�6_   $��14(�6s   5      5      