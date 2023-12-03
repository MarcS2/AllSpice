<template>
  <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <div v-if="showHeader" class="modal-header">
          <h5 class="modal-title" :id="modalId + 'Label'">
            <slot name="modalTitle">

            </slot>
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <!-- <div class="modal-body"> -->
        <slot name="modalBody"></slot>
        <!-- </div> -->
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger";
export default {
  props: {
    modalId: { type: String, required: true },
    modalSize: { type: String, default: '' },
    showHeader: { type: Boolean, default: false }
  },
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped></style>