<?php 
function theme_files() {
    // (Уникальное имя стиля, подключение файла с стилем, массив идентификаторов, автообновление версии, медиа, для которого предназначен стиль)
    wp_register_style('theme_reset', get_template_directory_uri() . '/css/reset.css', array(), filemtime(get_template_directory() . '/css/reset.css'));
    wp_register_style('theme_main', get_template_directory_uri() . '/css/main.css', array(), filemtime(get_template_directory() . '/css/main.css'));
    // Тоже самое, только с in_footer, указывает, загружать ли скрипт в подвале, по умолчанию скрипт загружается в голове
    
    wp_register_script('theme_script', get_template_directory_uri() . '/js/main.js', array(), filemtime(get_template_directory()) . '/js/main.js', $in_footer);

    // Подключение зарегистрированых стилей и скриптов
    wp_enqueue_style('theme_reset');
    wp_enqueue_style('theme_main');
    wp_enqueue_style('theme_script');

}
// Функция, которая привязывает функцию к опередленному действию
add_action('wp_enqueue_scripts', 'theme_files', 1);
function custom_excerpt_length($length){
    return 20;
}
add_filter('excerpt_length','custom_excerpt_length', 999);?>