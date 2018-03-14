<Application.Resources>

        <!--Copy ONLY inside of ResourceDictionary tag-->
        <ResourceDictionary>
            <!--Start ToggleSwitch-->
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/desktop;component/Controls/ToggleSwitch/Assets/Styles.xaml"/>
                <ResourceDictionary Source="pack://application:,,,/desktop;component/Controls/ToggleSwitch/Assets/ToggleSwitchStyles.xaml"/>
            </ResourceDictionary.MergedDictionaries>

            <BooleanToVisibilityConverter x:Key="VisibilityConverter"/>
            <!--End ToggleSwitch-->
    </Application.Resources>

	davat vzdy do gridu s column/row Auto