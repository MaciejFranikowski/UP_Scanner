   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics  5   	   E AUP_Scanner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UP_Scanner
UP_Scanner 5   	      UP_Scanner.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   @d2http://schemas.microsoft.com/expression/blend/2008      5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5   	   Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase      5   	   *localclr-namespace:UP_Scanner      5   	     � Title$  Scanner_WPF��5	   	   $	��500��6   $	��800��6*   .��5
      � .+�5   
   �� -   5         ��Name$ ScannerSource��5      $	��164��5      $��21��5      $��
21,0,0,372q�5      $
��Left=�5      $��Bottom=�5      ��5      �� *DeviceNames+ �� SelectedItem 5      �� *ChosenScannerName+�� 5   
   -   5         ��Name$ Skaner��5      $��0q�5      $��Bottom=�5      $��21,0,615,398q�5      $��24��5      $��Skaner:��5      �� 5   
   -   5        $��ScanType��5   ]   $��
21,0,0,259q�6   $
��Left=�6'   $��Bottom=�6B   $	��164��6m   $��24��6y   .@�5      ��  �� 
IsSelected$
 True��6   .��6-   Black White (1 bit)6B   �� 5      .��6   	Greyscale6&   �� 5      .��6   RGB6    5      �� 5   
   -   5        $ Resolutiontxt��5      $��0q�5       $��Bottom=�5!      $��21,0,615,343q�5"      $��24��5#      $��Rozdzielczość��5$      �� 5&   
   -   5        $��
Resolution��5&   ]   $��
21,0,0,319q�6   $
��Left=�6'   $��Bottom=�6B   $	��164��6o   $��24��6{   .@�5'      �� $
 True��6   .��6-   	100 dpi66   �� 5(      .��6   	200 dpi6$   �� 5)      .��6   	300 dpi6$   �� 5*      .��6   	400 dpi6$   �� 5+      .��6   	500 dpi6$   �� 5,      .��6   	600 dpi6$   5-      �� 5.   
   -   5         ��Name$ 	mainImage��5.      $��315,10,92,25q�6#   �� 5/   
   $��0q�50      $��Bottom=�51      $��21,0,615,288q�52      $��24��53      $��Rodzaj skanu:��54      �� 55   
   -   5        $	��136��55      $��20��56      $��	21,0,0,60q�57      $
��Left=�58      $��Bottom=�59      $��Skanuj��5:      6"   �� 5;   
   -   5        $	��136��5;      $��20��5<      $��	21,0,0,85q�5=      $
��Left=�5>      $��Bottom=�5?      $��Skanuj driver��5@      6)   �� 5A   
   -	   5        $	��136��5A      $��20��5B      $��	21,0,0,35q�5C      $
��Left=�5D      $��Bottom=�5E      $��Zapisz do pliku��5F      6+   �� 5G   
   -
   5        $	��136��5G      $��20��5H      $��
21,0,0,111q�5I      $
��Left=�5J      $��Bottom=�5K      $��Szukaj skanerów��5M      5N      5O      