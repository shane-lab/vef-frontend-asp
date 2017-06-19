<?
if (!isset($_GET['id'])) {
    return;
}

$uuid = $_GET['id'];
?>

  <!DOCTYPE html>
  <html>

  <head>
    <title>stream connection test - host:
      <?= $uuid; ?>
    </title>
    <script src="../../bower_components/webcomponentsjs/webcomponents-lite.js"></script>

    <link rel="import" href="../../bower_components/paper-material/paper-material.html">
    <link rel="import" href="./vef-videostreamer.html">
  </head>

  <body>
    <style is="custom-style">
      .paper-font-display1 {
        font-weight: 300 !important;
        color: teal;
        margin: 20px;
        @apply(--paper-font-display1);
      }
      
      .paper-font-headline {
        @apply(--paper-font-headline);
      }
      
      .paper-font-title {
        @apply(--paper-font-title);
      }
      
      .paper-font-subhead {
        @apply(--paper-font-subhead);
      }
      
      .paper-font-button {
        @apply(--paper-font-button);
        color: teal;
      }
      
      paper-material {
        min-height: 300px;
        position: relative;
        padding: 10px;
        margin: 10px 0;
        text-align: center;
      }
      
      .code-area pre {
        position: relative;
        left: 135px;
        text-align: left;
      }
      
      span.info {
        font-size: 12px;
        color: #aaa;
      }
    </style>

    <div class="paper-font-display1">&#x3C;vef-videostreamer&#x3E;</div>

    <paper-material elevation="1">
      <div class="paper-font-button">example implementation</div>
      <div class="code-area">
        <pre>
			&#x3C;!-- requires PEERJS (recommend firefox40+ fix) --&#x3E;

			&#x3C;!-- create a listener element --&#x3E;
			&#x3C;vef-videostreamer peer-id=&#x22;{{hostid}}&#x22;&#x3E;&#x3C;/vef-videostreamer&#x3E;&#x9;
		</pre>
      </div>

      <vef-videostreamer id="streamer" player-class="custom" width="900" peer-id="<?= $uuid; ?>"></vef-videostreamer>
    </paper-material>

  </body>

  </html>