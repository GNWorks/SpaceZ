   M S B A M L   `   `   `  ����K  GDSNCommandCenter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DSNCommandCenterDSNCommandCenter  5   	   !    DSNCommandCenter.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:DSNCommandCenter     5   	   W SFontAwesome5.Net, Version=2.1.6.0, Culture=neutral, PublicKeyToken=9cfaf01297a008f8/fa5%http://schemas.fontawesome.com/icons/  5   	     � Title$  SpaceZ��5	   	   $	��650��6   $
��1200��6%   .��5
      � ���#333238 � ColumnDefinitions 5   
   �� 5      -   5         �Name$ Sidebar��5      $��* �6.   �� 5      $��5* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   � -   5        $
��Menu��5      $��0��6   ���#333238 5      �� 5      $��2* �6    �� 5      $	��16* �6    .+�5      � 5      �� 5      -   5        $��SidebarItems��5      $��Disabled=�6+   $��0q�6�   ���#3b3c40$��1��6�   
���������5       �� 5!       �� 
TargetType" ��6    �� Triggers 5"      P� 5#       P� Property �@��
IsSelected P� Value . . �5$   "   ��  �� Property � ��	 �� Value
	 �����6?   5%      5'      .@�5*      �� -   5        
 ��Name$

 Home��5*      $��60��6-   .��5+      ��  �� Orientation$ 
Horizontal=�6!   .+�5,        FontAwesome5.SvgAwesome     
Foreground
 �����$��10,0,0,0q�6<   $
��20px��6N      Icon$ 
Solid_Home=�6[   $��Center=�6m   $��Center=�6�   �� 5-      $
��Home��6$   $
��Left=�60   $��Center=�6K   $��20 10q�6f   5.      5/      �� 50      -   5        $
 MissionControlSystem��50      $��60��6=   .��51      �� $ 
Horizontal=�6!   .+�52        
 �����$��10,0,0,0q�6<   $
��20px��6N   $ Solid_Keyboard=�6[   $��Center=�6q   $��Center=�6�   �� 53      $��MissionControlSystem��6$   $
��Left=�6@   $��Center=�6[   $��20 10q�6v   54      55      �� 56      -   5        $
 CommunicationSystem��56      $��60��6<   .��57      �� $ 
Horizontal=�6!   .+�58        
 �����$��10,0,0,0q�6<   $
��20px��6N   $ Solid_SatelliteDish=�6[   $��Center=�6v   $��Center=�6�   �� 59      $��CommunicationSystem��6$   $��Center=�6?   $��20 10q�6[   5:      5;      5<      5>      � 5A   
   $��1��6    5B      �� 5C      $
��70px��6    $��* �61   �� 5D      $��5* �6    .+�5H      � $��0��6   ���#333238.+�5I      �� $��Center=�6   
�������$��Center=�6L   $��22(�6g   DSN Command Center6�   5J      �� 5N      $��1��6   
�������._�5O      � -   5        $��ContentMain��5O      $��1��6*   $��
20,20,20,0q�67   
�������.+�5P      �� $��Center=�6    $��Center=�6=   $��18(�6X   $
��Bold"�6f   $��317,152,328,356.5q�6x   " Welcome to DSN Command Center..!6�   5Q      5R      5S      5T      5U      