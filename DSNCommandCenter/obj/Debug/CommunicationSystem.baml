   M S B A M L   `   `   `  ����K  GDSNCommandCenter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DSNCommandCenterDSNCommandCenter  5      *    $DSNCommandCenter.CommunicationSystem   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:DSNCommandCenter     5      .��5      � $��0,0,1,-1q�6   ���#333238.+�5	   
   �� -   5          ��Name$"  communicationSystemTabSystem��5	      .@�5
      �� $��Launch Vehicle��6   .��5      � ���#333238$��0,-5,0,0q�6,    � ColumnDefinitions 5      �� 5      $
��337* �6+   �� 5      $	��60* �6+   �� 5      $
��396* �6+   .+�5      �� $��16(�6   $
��Bold"�6*   
�������$)��#Launch Vehicle Communication System��6O   $
��Left=�6}   $��	11,21,0,0q�6�   $	��Top=�6�   �� 5      $
��Bold"�6   
�������$��Choose LaunchVehicle:��6B   $
��Left=�6b   $��	20,72,0,0q�6}   $	��Top=�6�    J� RenderTransformOrigin$ -2.033,0.618&�6�   �� 5      -   5         ��Name$ activeVehiclesDropDown��5   r   $
��Left=�6   $��
168,73,0,0q�6:   $	��Top=�6N   $	��472��6f   $��25��6�   $
��Bold"�6�   $��3��6�   �� 5      -   5         ��Name$ requestTelemetry��5   /   $
��Bold"�6   $��Request Telemetry��6I   $
��Left=�6�   $��
30,339,0,0q�6�   $	��Top=�6�   $	��141��6�   $��32��6�   �� 5      -   5         ��Name$ telemetryInfoBox��5      
�������$
��Left=�6L   $	��202��6g   $��
27,111,0,0q�6t   $	��Top=�6�   $	��615��6�   $��3��6�   �� 5      -   5        $ stopTelemetry��5      $
��Bold"�66   $��Stop Telemetry��6H   {�.  $
��Left=�6�   $��191,338,0,0q�6�   $	��Top=�6�   $	��141��6�   $ 
0.448,1.43&�6�   $��32��6  �� 5      -   5        $ deployPayload��5      $
��Bold"�66   $��2��6H   {�.  $��Deploy Payload��6n   $��1��6�   $
��Left=�6�   $��
23,338,0,0q�6�   $	��Top=�6�   $	��141��6�   $��32��6�   6�   5      5      �� 5      $��Payload Vehicle��6   .��5      � ���#333238.+�5      �� $��16(�6   $
��Bold"�6*   
�������$"��Payload Communication System��6O   $
��Left=�6v   $��	11,21,0,0q�6�   $	��Top=�6�   $��3��6�   �� 5      $
��Bold"�6   
�������$��Choose Payload Vehicle:��6B   $
��Left=�6d   $��6.832,72,0,0q�6   $	��Top=�6�   $ -2.033,0.618&�6�   $��2��6�   $��1��6�   �� 5      -   5        $# activePayloadVehiclesDropDown��5   r   $
��Left=�6   $��
162,72,0,0q�6:   $	��Top=�6N   $	��458��6f   $��25��6�   $
��Bold"�6�   �� 5       -   5        $ requestPayloadTelemetry��5    /   $
��Bold"�6   $��Request Telemetry��6P   $
��Left=�6�   $��16.832,339,0,0q�6�   $	��Top=�6�   $	��141��6�   $��32��6�   $��2��6�   $��1��6  �� 5!      -	   5        $ payloadTelemetryInfoBox��5!      
�������$
��Left=�6S   $	��202��6n   $��13.832,111,0,0q�6{   $	��Top=�6�   $	��299��6�   $��2��6�   $��1��6�   �� 5"      -
   5        $ stopPayloadTelemetry��5"      {�.  $
��Bold"�6O   $��Stop Telemetry��6a   $
��Left=�6�   $��172,339,0,0q�6�   $	��Top=�6�   $	��141��6�   $ 
0.448,1.43&�6�   $��32��6  �� 5#      -   5        $ payloadDataInfoBox��5#      $
��Left=�6:   $	��203��6U   $��328,108,0,0q�6b    �� TextWrapping$
 Wrap=�6w   $	��Top=�6�   $	��288��6�   �� 5$      -   5        $ requestPayloadData��5$      $
��Bold"�6:   $��
Start Data��6L   $
��Left=�6a   $��333,337,0,0q�6|   $	��Top=�6�   $	��141��6�   $ 0.447,1.547&�6�   $��32��6�   6�   �� 5%      -   5        $ stopPayloadData��5%      {�.  $��	Stop Data��6I   $
��Bold"�6]   $
��Left=�6o   $��489,336,0,0q�6�   $	��Top=�6�   $	��131��6�   $��32��6�   6�   �� 5&      -   5        $ decommisionPayload��5&      $
��Bold"�6;   $��Decommision��6M   $
��Left=�6c   $��261,383,0,0q�6~   $	��Top=�6�   $	��114��6�   $��31��6�   6�   5'      5(      5)      5*      5+      