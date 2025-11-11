
<?php get_header() ?>
<main class = "main">
    <section class = "content">
        <?php if (have_posts()) : while (have_posts()) : the_post(); ?>
        <div class="background">
            <div class = "content_item">
                <p>Автор: <?php the_author(); ?></p>
                <p>Категория: <?php the_category(', '); ?></p>
                <h2 class = "content_item_title"><a href = "<?php the_permalink(); ?>"><?php the_title(); ?></a></h2>
                <?php the_excerpt() ?>
            </div>
        </div>
        <?php endwhile; else : ?>
            <p>Записи отсутствуют.</p>
        <?php endif; ?>
        
    </section>
   <section class="categories">
    <h2 class="categories_title">Категории</h2>
    <ul class="categories_list">
        <?php $args = array(
            'orderby' => 'name',
            'order' => 'ASC',
            'hide_empty' => 0
        );?>
        <?php $categories = get_categories($args); ?>
        <?php foreach ($categories as $category) { 
            echo '<li><a href="' . get_category_link($category->term_id) . '" title="' . sprintf(__("Просмотреть все записи в рубрике '%s'", 'your-theme'), $category->name) . '">' . $category->name . '</a></li>';
        }?>
    </ul>
</section>
</main>
<?php get_footer() ?>