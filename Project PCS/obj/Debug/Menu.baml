   M S B A M L   `   `   `  ����F  BProject PCS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Project_PCSProject_PCS  5   	       Project_PCS.Menu   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Project_PCS     5   	     � Title$
  Menu��5   	   $	��460��6   $	��800��6#    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  J� RenderTransformOrigin$ 0.494,0.565&�6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      .+�5   
   � ���#3B4D61$��2��5       � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       �� 5!      �� 5"      .+�5$      �� $��
MENU ADMIN��6   $��1��5%      $��2��5&      
�������$��Center=�5(      $��Center=�5)      $��Fonts/#Dosis)�5*      $��Medium"�5+      �� 5-      -   5        $��LogOut��54      $��Log Out��5-      
�������$��10(�5/      $��Medium"�50      $��0,0,10,0q�51      $��12��52      $��2��53      ��56      �� 57       �� 
TargetType" ��6   .�58      ��  �� Property � ��	 �� Value$	 Fonts/#Dosis)�6C   �� 59       � ��	 �#ef9d1067   �� 5:       �@�Cursor$
	 Hand��63   
 �� Triggers
 5;      P� 5<       P� Property �@J�IsMouseOver P� Value . . �5=   "   ��  � ��
	 �  ��6?   5>      5@      ��5C      �� 5D       �� 
TargetType" ��6&   .R�5E      �� $��75��6!   $��15��6,    �� CornerRadius$ 8��5F   %   #��z��5G   %   ._�5H      �� $��Center=�6/   $��Center=�5I   3   5J      5K      5M      5N      � 5Q   
   -   5         �Name$ gridMenu��5Q      $��1��6   $
��Left=�6.   $	��300��6I   $��
132,87,0,0q�6V   $	��Top=�6j   $	��299��6�   .+�5S      �� -   5        ��5T      �� 5U      .R�5V      � .+�5W      F� ���	#FF3B4D61��5X   "   B� 5Y   &   .��5Z   *   E�  E� 
StartPoint$ 150,0&�65   .��5[   .   ��  �� 
IsLargeArc .  �� Size$ 50, 50��5\       �� Point$ 150, 300&�5]       �� SweepDirection$ Counterclockwise=�5^      5_   +   5`   '   5b      �� 5c      $��10,120,170,128q�6(   $��34(�6@    �� TextAlignment$ Center=�6N   $��Center=�6e   
�������$	��123��6�   $��Fonts/#Dosis)�6�   Master6�   5d      5e      5g      �� 5h      -   5        ��5i      �� 5j      .R�5k      � .+�5l      F� ���	#FF6B7B8C��5m   "   B� 5n   &   .��5o   *   E� $ 150,0&�65   .��5p   .   ��  . $ 50, 50��5q      $ 150, 300&�5r      $ 	Clockwise=�5s      5t   +   5u   '   5w      �� 5x      $��160,120,10,83.8q�6(   $��34(�6A   $ Center=�6O   $��Center=�6f   
�������$	��126��6�   $��Fonts/#Dosis)�6�   Report6�   5y      5z      5|      ��5~      
� 5      .��5�      �  � Color$ 	#FF3B4D61��6#    � Offset$ 1Q�65   � 5�      $ White��6#   $ 1Q�61   � 5�      $ 	#FF3B4D61��6#   $ 0.979Q�65   5�      5�      �� 5�   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��32,10,340.6,360.4q�5�      .+�5�      �� $��60��6   $��60��6   $��Images/music.png��5�       �� BitmapScalingMode$
 Fant=�5�      �� 5�      $��ADMIN��6   $��40(�5�      $
��Bold"�5�      $��Center=�5�      $��Fonts/#Dosis)�5�      $��	30,0,30,0q�5�      �� 5�      $��60��6   $��60��6   $��Images/music.png��5�      $
 Fant=�5�      5�      � 5�   
   $��2��5�      $��533,-400,-983,120q�5�       K� ZIndex$ -1��5�      z�5�      �� 5�       �� Angle$ 50Q�6"   .+�5�      �� $ 
Horizontal=�6   $��-350,250,-255,-250q�5�      .+�5�      �� .+�5�      �� $	��700��6   $��35��5�      ���#6B7B8C$��	150,0,0,0q�5�      $ 20��5�      �� 5�      $	��700��6   $��35��5�      ���#3B4D61$��0,15,0,0q�5�      $ 20��5�      �� 5�      $	��700��6   $��35��5�      ���#EF9D10$��	75,15,0,0q�5�      $ 20��5�      5�      �� 5�      .+�5�      �� $	��700��6   $��35��5�      ���#EF9D10$��	150,0,0,0q�5�      $ 20��5�      �� 5�      $	��700��6   $��35��5�      ���#6B7B8C$��0,15,0,0q�5�      $ 20��5�      �� 5�      $	��700��6   $��35��5�      ���#3B4D61$��	75,15,0,0q�5�      $ 20��5�      5�      5�      5�      5�      5�      