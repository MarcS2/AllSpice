<template>
  <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" :id="modalId + 'Label'">
            <slot name="modalTitle">

            </slot>
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <slot name="modalBody"></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  props: {
    modalId: { type: String, required: true },
    modalSize: { type: String, default: '' }
  },
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped></style>