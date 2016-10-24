<h1>sgQuestion post check</h1>
ポストされた内容を確認する

<?php
    $name = '';
    $join = '';
    $horoscope = '';
    $memo = '';

    if ( isset($_POST['name'] )) $name = $_POST['name'];
    if ( isset($_POST['join'] )) $join = $_POST['join'];
    if ( isset($_POST['horoscope'] )) $horoscope = $_POST['horoscope'];
    if ( isset($_POST['memo'] )) $memo = $_POST['memo'];
?>

<p>name: <?php echo $name; ?></p>
<p>join: <?php echo $join; ?></p>
<p>horoscope: <?php echo $horoscope; ?></p>
<p>memo: <?php echo $memo; ?></p>

    

